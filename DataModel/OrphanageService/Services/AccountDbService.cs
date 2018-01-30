using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
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

        public AccountDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
        }

        public async Task<OrphanageDataModel.FinancialData.Account> GetAccount(int Aid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var account = await dbContext.Accounts.AsNoTracking()
                    .Include(a => a.Bails)
                    .Include(b => b.Guarantors)
                    .FirstOrDefaultAsync(a => a.Id == Aid);

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
                var guarantors = await(from guar in dbContext.Guarantors.AsNoTracking()
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
                var bails = await(from bail in dbContext.Bails.AsNoTracking()
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
    }
}
