using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Account
{
    public class AccountsViewModel
    {
        public ObservableCollection<AccountModel> Accounts { get; set; }
        private IList<OrphanageDataModel.FinancialData.Account> _SourceAccounts;

        private readonly IApiClient _apiClient;
        private readonly IMapperService _mapperService;
        private readonly ITranslateService _translateService;
        private readonly IDataFormatterService _dataFormatterService;

        public event EventHandler DataLoaded;

        public AccountsViewModel(IApiClient apiClient, IMapperService mapperService,
            ITranslateService translateService, IDataFormatterService dataFormatterService)
        {
            _apiClient = apiClient;
            _mapperService = mapperService;
            _translateService = translateService;
            _dataFormatterService = dataFormatterService;
        }

        public async void LoadAccounts()
        {
            var accoutnsCount = await _apiClient.AccountsController_GetAccountsCountAsync();
            var ReturnedAccoutns = await _apiClient.AccountsController_GetAllAsync(accoutnsCount, 0);

            _SourceAccounts = ReturnedAccoutns;

            Accounts = new ObservableCollection<AccountModel>(_mapperService.MapToAccountModel(_SourceAccounts));

            DataLoaded?.Invoke(this, new EventArgs());
        }

        public async void LoadAccounts(IEnumerable<int> bailsIds)
        {
            var ReturnedAccoutns = await _apiClient.AccountsController_GetByIdsAsync(bailsIds);

            _SourceAccounts = ReturnedAccoutns;

            Accounts = new ObservableCollection<AccountModel>(_mapperService.MapToAccountModel(_SourceAccounts));

            DataLoaded?.Invoke(this, new EventArgs());
        }

        public async void Update(int accountId)
        {
            var sourceAccount = await _apiClient.AccountsController_GetAsync(accountId);

            var sourceAccountToReplace = _SourceAccounts.FirstOrDefault(b => b.Id == accountId);
            var sourceAccountIndex = _SourceAccounts.IndexOf(sourceAccountToReplace);
            _SourceAccounts[sourceAccountIndex] = sourceAccount;

            var accountModel = _mapperService.MapToAccountModel(sourceAccount);

            var accountToEdit = Accounts.FirstOrDefault(c => c.Id == accountId);
            var accountToEditIndex = Accounts.IndexOf(accountToEdit);
            Accounts[accountToEditIndex] = accountModel;
        }

        public async Task<bool> Delete(int accountId, bool ForceDelete)
        {
            var account = _SourceAccounts.FirstOrDefault(c => c.Id == accountId);
            if (account == null)
                return false;
            await _apiClient.AccountsController_DeleteAsync(accountId);
            return true;
        }

        public async Task<IEnumerable<int>> BailsIds(int accountId)
        {
            return (await _apiClient.AccountsController_GetBailsAsync(accountId)).Select(b => b.Id);
        }

        public async Task<IEnumerable<int>> BailsIds(IEnumerable<int> accountsIds)
        {
            IList<int> returnedBailsIds = new List<int>();
            foreach (var id in accountsIds)
            {
                var bailIds = await BailsIds(id);
                foreach (var bailId in bailIds)
                    returnedBailsIds.Add(bailId);
            }
            return returnedBailsIds;
        }

        public async Task<IList<OrphanageDataModel.FinancialData.Bail>> Bails(int accountId)
        {
            return await _apiClient.AccountsController_GetBailsAsync(accountId);
        }

        public async Task<IEnumerable<int>> GuarantorsIds(int accountId)
        {
            return (await _apiClient.AccountsController_GetGuarantorsAsync(accountId)).Select(a => a.Id);
        }

        public async Task<IEnumerable<int>> GuarantorsIds(IEnumerable<int> accountsIds)
        {
            IList<int> returnedGuarantorsIds = new List<int>();
            foreach (var id in accountsIds)
            {
                var guarantorIds = await GuarantorsIds(id);
                foreach (var guarantorId in guarantorIds)
                    returnedGuarantorsIds.Add(guarantorId);
            }
            return returnedGuarantorsIds;
        }

        public async Task<IList<OrphanageDataModel.Persons.Guarantor>> Guarantors(int accountId)
        {
            return await _apiClient.AccountsController_GetGuarantorsAsync(accountId);
        }

        public OrphanageDataModel.FinancialData.Account GetSourceAccount(int accountModelId) =>
            _SourceAccounts.FirstOrDefault(c => c.Id == accountModelId);
    }
}