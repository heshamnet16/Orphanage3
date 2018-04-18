using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System.Drawing;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Family
{
    public class FamilyEditViewModel
    {
        private readonly IApiClient _apiClient;
        private readonly Main.MainViewModel _mainViewModel;
        private readonly IExceptionHandler _exceptionHandler;
        private OrphanageDataModel.RegularData.Family _CurrentFamily = null;

        public FamilyEditViewModel(IApiClient apiClient, Main.MainViewModel mainViewModel, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _mainViewModel = mainViewModel;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<bool> Save(OrphanageDataModel.RegularData.Family family)
        {
            try
            {
                family.FamilyCardImagePage1Data = null;
                family.FamilyCardImagePage2Data = null;
                await _apiClient.FamiliesController_PutAsync(family);
                return true;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }

        public async Task<OrphanageDataModel.RegularData.Family> getFamily(int Cid)
        {
            var returnedFamily = await _apiClient.FamiliesController_GetAsync(Cid);
            var FamilyCardP1Task = _apiClient.GetImageData(returnedFamily.FamilyCardImagePage1URI);
            var FamilyCardP2Task = _apiClient.GetImageData(returnedFamily.FamilyCardImagePage2URI);
            returnedFamily.FamilyCardImagePage1Data = await FamilyCardP1Task;
            returnedFamily.FamilyCardImagePage2Data = await FamilyCardP2Task;
            _CurrentFamily = returnedFamily;
            return returnedFamily;
        }

        public async Task<bool> SaveImage(string url, Image image)
        {
            try
            {
                var ret = await _apiClient.SetImage(url, image);
                return ret;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }

        public async Task<bool> Save()
        {
            return await Save(_CurrentFamily);
        }

        public async Task<OrphanageDataModel.RegularData.Family> Add(OrphanageDataModel.RegularData.Family family)
        {
            if (family != null)
            {
                try
                {
                    family.UserId = Program.CurrentUser.Id;
                    var fam = (OrphanageDataModel.RegularData.Family)await _apiClient.FamiliesController_PostAsync(family);
                    return fam ?? null;
                }
                catch (ApiClientException apiEx)
                {
                    var retObject = await _exceptionHandler.HandleApiPostFunctionsAndShowErrors(getFamily, apiEx);
                    if (retObject == null)
                    {
                        retObject = await _exceptionHandler.HandleApiPostFunctions(getFamily, apiEx);
                        if (retObject != null)
                        { _mainViewModel.ShowFamilyEditView(retObject.Id); }
                        return null;
                    }
                    else
                        return retObject;
                }
            }
            return null;
        }
    }
}