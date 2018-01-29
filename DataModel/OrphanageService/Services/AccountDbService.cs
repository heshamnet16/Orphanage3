using AutoMapper;
using OrphanageService.DataContext;
using OrphanageService.DataContext.FinancialData;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using OrphanageService.DataContext.Persons;

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

        public async Task<AccountDto> GetAccountDto(int Aid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var account = await dbContext.Accounts.AsNoTracking()
                    .Include(a => a.Bails)
                    .Include(b => b.Guarantors)
                    .FirstOrDefaultAsync(a => a.Id == Aid);

                _selfLoopBlocking.BlockAccountSelfLoop(ref account);
                AccountDto bailDto = Mapper.Map<AccountDto>(account);
                return bailDto;
            }
        }

        public async Task<IEnumerable<AccountDto>> GetAccounts(int pageSize, int pageNum)
        {
            IList<AccountDto> accountsList = new List<AccountDto>();
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
                    AccountDto bailDto = Mapper.Map<AccountDto>(accountToFill);
                    accountsList.Add(bailDto);
                }
            }
            return accountsList;
        }

        public async Task<IEnumerable<GuarantorDto>> GetGuarantors(int Aid)
        {
            IList<GuarantorDto> returnedGuarantors = new List<GuarantorDto>();
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
                    var guarantorDto = Mapper.Map<OrphanageDataModel.Persons.Guarantor, GuarantorDto>(guarantorToFill);
                    returnedGuarantors.Add(guarantorDto);
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

        public async Task<IEnumerable<BailDto>> GetBails(int Aid)
        {
            IList<BailDto> returnedBails = new List<BailDto>();
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
                    var bailDto = Mapper.Map<OrphanageDataModel.FinancialData.Bail, BailDto>(bailToFill);
                    returnedBails.Add(bailDto);
                }
            }
            return returnedBails;
        }
    }
}
