using OrphanageDataModel.FinancialData;
using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class AccountDbService : IAccountDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;
        private readonly ILogger _logger;

        public AccountDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator, ILogger logger)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
            _logger = logger;
        }

        public async Task<OrphanageDataModel.FinancialData.Account> GetAccount(int Aid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var account = await dbContext.Accounts.AsNoTracking()
                    .Include(a => a.Bails)
                    .Include(b => b.Guarantors)
                    .FirstOrDefaultAsync(a => a.Id == Aid);
                if (account == null) return null;
                _selfLoopBlocking.BlockAccountSelfLoop(ref account);
                return account;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Account>> GetAccounts(int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.FinancialData.Account> accountsList = new List<OrphanageDataModel.FinancialData.Account>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int accountsCount = await _orphanageDBC.Bails.AsNoTracking().CountAsync();
                if (accountsCount < totalSkiped)
                {
                    totalSkiped = accountsCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var accounts = await _orphanageDBC.Accounts.AsNoTracking()
                    .OrderBy(c => c.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(a => a.Bails)
                    .Include(b => b.Guarantors)
                    .ToListAsync();

                foreach (var account in accounts)
                {
                    OrphanageDataModel.FinancialData.Account accountToFill = account;
                    _selfLoopBlocking.BlockAccountSelfLoop(ref accountToFill);
                    accountsList.Add(accountToFill);
                }
            }
            return accountsList;
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int Aid)
        {
            IList<OrphanageDataModel.Persons.Guarantor> returnedGuarantors = new List<OrphanageDataModel.Persons.Guarantor>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var guarantors = await (from guar in dbContext.Guarantors.AsNoTracking()
                                        where guar.AccountId == Aid
                                        select guar)
                    .Include(g => g.Address)
                    .Include(c => c.Name)
                    .Include(g => g.Account)
                              .ToListAsync();

                foreach (var guarantor in guarantors)
                {
                    var guarantorToFill = guarantor;
                    _selfLoopBlocking.BlockGuarantorSelfLoop(ref guarantorToFill);
                    returnedGuarantors.Add(guarantorToFill);
                }
            }
            return returnedGuarantors;
        }

        public async Task<int> GetAccountsCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int accountsCount = await _orphanageDBC.Accounts.AsNoTracking().CountAsync();
                return accountsCount;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Aid)
        {
            IList<OrphanageDataModel.FinancialData.Bail> returnedBails = new List<OrphanageDataModel.FinancialData.Bail>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var bails = await (from bail in dbContext.Bails.AsNoTracking()
                                   where bail.AccountID == Aid
                                   select bail)
                                .Include(b => b.Account)
                                .Include(b => b.Guarantor)
                              .ToListAsync();

                foreach (var bail in bails)
                {
                    var bailToFill = bail;
                    _selfLoopBlocking.BlockBailSelfLoop(ref bailToFill);
                    returnedBails.Add(bailToFill);
                }
            }
            return returnedBails;
        }

        public async Task<OrphanageDataModel.FinancialData.Account> AddAccount(OrphanageDataModel.FinancialData.Account accountToAdd)
        {
            _logger.Information($"Trying to add new Account");
            if (accountToAdd == null)
            {
                _logger.Error($"the parameter object accountToAdd is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            using (var orphanageDBC = new OrphanageDbCNoBinary())
            {
                using (var Dbt = orphanageDBC.Database.BeginTransaction())
                {
                    orphanageDBC.Accounts.Add(accountToAdd);
                    var ret = await orphanageDBC.SaveChangesAsync();
                    if (ret >= 1)
                    {
                        Dbt.Commit();
                        _logger.Information($"new Account object with id{accountToAdd.Id} has been added");
                        _selfLoopBlocking.BlockAccountSelfLoop(ref accountToAdd);
                        _logger.Information($"the Account object with id{accountToAdd.Id} will be returned");
                        return accountToAdd;
                    }
                    else
                    {
                        Dbt.Rollback();
                        _logger.Information($"something went wrong, nothing was added, null will be returned");
                        return null;
                    }
                }
            }
        }

        public async Task<bool> SaveAccount(OrphanageDataModel.FinancialData.Account accountToSave)
        {
            _logger.Information($"Trying to save Account");
            if (accountToSave == null)
            {
                _logger.Error($"the parameter object accountToSave is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            using (OrphanageDbCNoBinary orphanageDc = new OrphanageDbCNoBinary())
            {
                int ret = 0;
                orphanageDc.Configuration.LazyLoadingEnabled = true;
                orphanageDc.Configuration.ProxyCreationEnabled = true;
                orphanageDc.Configuration.AutoDetectChangesEnabled = true;

                var orginalAccount = await orphanageDc.Accounts.
                    FirstOrDefaultAsync(m => m.Id == accountToSave.Id);

                if (orginalAccount == null)
                {
                    _logger.Error($"the original account object with id {accountToSave.Id} object is not founded, ObjectNotFoundException will be thrown");
                    throw new Exceptions.ObjectNotFoundException();
                }

                orginalAccount.AccountName = accountToSave.AccountName;
                orginalAccount.Amount = accountToSave.Amount;
                orginalAccount.CanNotBeNegative = accountToSave.CanNotBeNegative;
                orginalAccount.Currency = accountToSave.Currency;
                orginalAccount.CurrencyShortcut = accountToSave.CurrencyShortcut;
                orginalAccount.Note = accountToSave.Note;
                ret += await orphanageDc.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"the object account with id {orginalAccount.Id} has been saved successfully, {ret} changes has been made ");
                    return true;
                }
                else
                {
                    _logger.Information($"the object account with id {orginalAccount.Id} has been saved successfully, nothing has changed");
                    return false;
                }
            }
        }

        public async Task<bool> DeleteAccount(int accountID)
        {
            if (accountID <= 0)
            {
                _logger.Error($"the integer parameter accountID less than zero, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            using (var orphanageDb = new OrphanageDbCNoBinary())
            {
                using (var dbT = orphanageDb.Database.BeginTransaction())
                {
                    var account = await orphanageDb.Accounts.Where(c => c.Id == accountID)
                        .Include(b => b.Bails)
                        .Include(b => b.Guarantors)
                        .FirstOrDefaultAsync();

                    if (account == null)
                    {
                        _logger.Error($"the original account object with id {accountID} is not founded, ObjectNotFoundException will be thrown");
                        throw new Exceptions.ObjectNotFoundException();
                    }
                    if (account.Guarantors != null && account.Guarantors.Count > 0)
                    {
                        //the account has another guarantors
                        _logger.Error($"the account object with id {accountID} has not null foreign key on Guarantors table, HasForeignKeyException will be thrown");
                        throw new Exceptions.HasForeignKeyException(typeof(OrphanageDataModel.FinancialData.Account), typeof(OrphanageDataModel.Persons.Guarantor));
                    }
                    if (account.Bails != null && account.Bails.Count > 0)
                    {
                        //the account has another bails
                        _logger.Error($"the account object with id {accountID} has not null foreign key on Bails table, HasForeignKeyException will be thrown");
                        throw new Exceptions.HasForeignKeyException(typeof(OrphanageDataModel.FinancialData.Account), typeof(OrphanageDataModel.FinancialData.Bail));
                    }

                    orphanageDb.Accounts.Remove(account);
                    if (await orphanageDb.SaveChangesAsync() > 1)
                    {
                        dbT.Commit();
                        _logger.Information($"the account object with id {accountID} has been successfully removed");
                        return true;
                    }
                    else
                    {
                        dbT.Rollback();
                        _logger.Information($"something went wrong, account with id ({accountID}) was not be removed, false will be returned");
                        return false;
                    }
                }
            }
        }
    }
}