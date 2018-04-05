using OrphanageV3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Bail
{
    public class BailEditViewModel
    {
        private readonly IApiClient _apiClient;

        private OrphanageDataModel.FinancialData.Bail _CurrentBail = null;

        public BailEditViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<bool> Save(OrphanageDataModel.FinancialData.Bail bail)
        {
            await _apiClient.BailsController_PutAsync(bail);
            return true;
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
                var retBail = (OrphanageDataModel.FinancialData.Bail)await _apiClient.BailsController_PostAsync(bail);
            }
            catch (ApiClientException apiEx)
            {
                //Created
                if (apiEx.StatusCode == "201")
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<OrphanageDataModel.FinancialData.Bail>(apiEx.Response) ?? null;
                return null;
            }
            return null;
        }
    }
}