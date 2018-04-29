using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System.Drawing;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Caregiver
{
    public class CaregiverEditViewModel
    {
        private readonly IApiClient _apiClient;
        private readonly Main.MainViewModel _mainViewModel;
        private readonly IExceptionHandler _exceptionHandler;
        private OrphanageDataModel.Persons.Caregiver _CurrentCaregiver = null;

        public CaregiverEditViewModel(IApiClient apiClient, Main.MainViewModel mainViewModel, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _mainViewModel = mainViewModel;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<bool> Save(OrphanageDataModel.Persons.Caregiver caregiver)
        {
            caregiver.IdentityCardPhotoBackData = null;
            caregiver.IdentityCardPhotoFaceData = null;
            try
            {
                await _apiClient.Caregivers_PutAsync(caregiver);
                return true;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }

        public async Task<OrphanageDataModel.Persons.Caregiver> getCaregiver(int Cid)
        {
            var returnedCaregiver = await _apiClient.Caregivers_GetAsync(Cid);
            var fronPhotoTask = _apiClient.GetImageData(returnedCaregiver.IdentityCardImageFaceURI);
            var backPhotoTask = _apiClient.GetImageData(returnedCaregiver.IdentityCardImageBackURI);
            returnedCaregiver.IdentityCardPhotoFaceData = await fronPhotoTask;
            returnedCaregiver.IdentityCardPhotoBackData = await backPhotoTask;
            _CurrentCaregiver = returnedCaregiver;
            return returnedCaregiver;
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
            return await Save(_CurrentCaregiver);
        }

        public async Task<OrphanageDataModel.Persons.Caregiver> Add(OrphanageDataModel.Persons.Caregiver caregiver)
        {
            caregiver.IdentityCardPhotoBackData = null;
            caregiver.IdentityCardPhotoFaceData = null;

            try
            {
                caregiver.UserId = Program.CurrentUser.Id;
                var retOrp = (OrphanageDataModel.Persons.Caregiver)await _apiClient.Caregivers_PostAsync(caregiver);
            }
            catch (ApiClientException apiEx)
            {
                var retObject = await _exceptionHandler.HandleApiPostFunctionsAndShowErrors(getCaregiver, apiEx);
                if (retObject == null)
                {
                    retObject = await _exceptionHandler.HandleApiPostFunctions(getCaregiver, apiEx);
                    if (retObject != null)
                    { _mainViewModel.ShowCaregiverEditView(retObject.Id); }
                    return null;
                }
                else
                    return retObject;
            }
            return null;
        }
    }
}