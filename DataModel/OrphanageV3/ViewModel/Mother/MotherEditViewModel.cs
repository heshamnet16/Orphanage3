using OrphanageV3.Services;
using System.Drawing;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Mother
{
    public class MotherEditViewModel
    {
        private readonly IApiClient _apiClient;
        private Size _ImageSize = new Size(153, 126);
        public Size ImagesSize { get => _ImageSize; set { _ImageSize = value; } }

        private OrphanageDataModel.Persons.Mother _CurrentMother = null;

        public MotherEditViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
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
            catch (ApiClientException apiException)
            {
                if (apiException.StatusCode != "304")
                {
                    //TODO Status Message not changed
                    //TODO Bad request error handling
                    return false;
                }
                return true;
            }
        }

        public async Task<OrphanageDataModel.Persons.Mother> getMother(int Cid)
        {
            var returnedMother = await _apiClient.MothersController_GetAsync(Cid);
            var fronPhotoTask = _apiClient.GetImageData(returnedMother.IdentityCardFaceURI, _ImageSize, 50);
            var backPhotoTask = _apiClient.GetImageData(returnedMother.IdentityCardBackURI, _ImageSize, 50);
            returnedMother.IdentityCardPhotoFaceData = await fronPhotoTask;
            returnedMother.IdentityCardPhotoBackData = await backPhotoTask;
            _CurrentMother = returnedMother;
            return returnedMother;
        }

        public async Task<bool> SaveImage(string url, Image image)
        {
            var ret = await _apiClient.SetImage(url, image);
            return ret;
        }

        public async Task<bool> Save()
        {
            return await Save(_CurrentMother);
        }
    }
}