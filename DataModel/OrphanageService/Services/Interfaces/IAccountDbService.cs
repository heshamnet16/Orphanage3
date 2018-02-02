﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IAccountDbService
    {
        Task<OrphanageDataModel.FinancialData.Account> GetAccount(int Aid);

        Task<IEnumerable<OrphanageDataModel.FinancialData.Account>> GetAccounts(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Aid);

        Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int Aid);

        Task<int> GetAccountsCount();
    }
}