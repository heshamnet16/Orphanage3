using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrphanageV3.ViewModel.Bail
{
    public class BailEditViewModel
    {
        private readonly IApiClient _apiClient;
        private readonly Main.MainViewModel _mainViewModel;
        private readonly IExceptionHandler _exceptionHandler;
        private OrphanageDataModel.FinancialData.Bail _CurrentBail = null;

        public BailEditViewModel(IApiClient apiClient, Main.MainViewModel mainViewModel, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _mainViewModel = mainViewModel;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<bool> Save(OrphanageDataModel.FinancialData.Bail bail)
        {
            try
            {
                await _apiClient.BailsController_PutAsync(bail);
                return true;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }

        public async Task<OrphanageDataModel.FinancialData.Bail> getBail(int Bid)
        {
            var returnedBail = await _apiClient.BailsController_GetAsync(Bid);
            _CurrentBail = returnedBail;
            return returnedBail;
        }

        public async Task<bool> Save()
        {
            return await Save(_CurrentBail);
        }

        public async Task<OrphanageDataModel.FinancialData.Bail> Add(OrphanageDataModel.FinancialData.Bail bail)
        {
            try
            {
                bail.UserId = Program.CurrentUser.Id;
                var retBail = (OrphanageDataModel.FinancialData.Bail)await _apiClient.BailsController_PostAsync(bail);
            }
            catch (ApiClientException apiEx)
            {
                var retObject = await _exceptionHandler.HandleApiPostFunctionsAndShowErrors(getBail, apiEx);
                if (retObject == null)
                {
                    retObject = await _exceptionHandler.HandleApiPostFunctions(getBail, apiEx);
                    if (retObject != null)
                    { _mainViewModel.ShowBailEditView(retObject.Id); }
                    return null;
                }
                else
                    return retObject;
            }

            return null;
        }
    }
}