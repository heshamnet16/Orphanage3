using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Account
{
    public class AccountEditViewModel
    {
        private readonly IApiClient _apiClient;
        private readonly IExceptionHandler _exceptionHandler;
        private OrphanageDataModel.FinancialData.Account _CurrentAccount = null;

        public AccountEditViewModel(IApiClient apiClient, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<bool> Save(OrphanageDataModel.FinancialData.Account account)
        {
            try
            {
                await _apiClient.Accounts_PutAsync(account);
                return true;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }

        public async Task<OrphanageDataModel.FinancialData.Account> getAccount(int Aid)
        {
            var returnedAccount = await _apiClient.Accounts_GetAsync(Aid);
            _CurrentAccount = returnedAccount;
            return returnedAccount;
        }

        public async Task<bool> Save()
        {
            return await Save(_CurrentAccount);
        }

        public async Task<OrphanageDataModel.FinancialData.Account> Add(OrphanageDataModel.FinancialData.Account account)
        {
            try
            {
                account.UserId = Program.CurrentUser.Id;
                var retBail = (OrphanageDataModel.FinancialData.Account)await _apiClient.Accounts_PostAsync(account);
            }
            catch (ApiClientException apiEx)
            {
                return await _exceptionHandler.HandleApiPostFunctions(getAccount, apiEx);
            }

            return null;
        }
    }
}