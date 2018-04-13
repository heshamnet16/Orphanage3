using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System.Drawing;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Mother
{
    public class MotherEditViewModel
    {
        private readonly IApiClient _apiClient;
        private readonly IExceptionHandler _exceptionHandler;
        private OrphanageDataModel.Persons.Mother _CurrentMother = null;

        public MotherEditViewModel(IApiClient apiClient, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<bool> Save(OrphanageDataModel.Persons.Mother mother)
        {
            try
            {
                mother.IdentityCardPhotoBackData = null;
                mother.IdentityCardPhotoFaceData = null;
                await _apiClient.MothersController_PutAsync(mother);
                return true;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }

        public async Task<OrphanageDataModel.Persons.Mother> getMother(int Cid)
        {
            var returnedMother = await _apiClient.MothersController_GetAsync(Cid);
            var fronPhotoTask = _apiClient.GetImageData(returnedMother.IdentityCardFaceURI);
            var backPhotoTask = _apiClient.GetImageData(returnedMother.IdentityCardBackURI);
            returnedMother.IdentityCardPhotoFaceData = await fronPhotoTask;
            returnedMother.IdentityCardPhotoBackData = await backPhotoTask;
            _CurrentMother = returnedMother;
            return returnedMother;
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
            return await Save(_CurrentMother);
        }
    }
}