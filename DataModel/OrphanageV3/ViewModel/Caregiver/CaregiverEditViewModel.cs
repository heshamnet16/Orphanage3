using OrphanageV3.Services;
using System.Drawing;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Caregiver
{
    public class CaregiverEditViewModel
    {
        private readonly IApiClient _apiClient;
        private Size _ImageSize = new Size(153, 126);
        public Size ImagesSize { get => _ImageSize; set { _ImageSize = value; } }

        private OrphanageDataModel.Persons.Caregiver _CurrentCaregiver = null;

        public CaregiverEditViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<bool> Save(OrphanageDataModel.Persons.Caregiver caregiver)
        {
            try
            {
                caregiver.IdentityCardPhotoBackData = null;
                caregiver.IdentityCardPhotoFaceData = null;
                await _apiClient.CaregiversController_PutAsync(caregiver);
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

        public async Task<OrphanageDataModel.Persons.Caregiver> getCaregiver(int Cid)
        {
            var returnedCaregiver = await _apiClient.CaregiversController_GetAsync(Cid);
            var fronPhotoTask = _apiClient.GetImageData(returnedCaregiver.IdentityCardImageFaceURI, _ImageSize, 50);
            var backPhotoTask = _apiClient.GetImageData(returnedCaregiver.IdentityCardImageBackURI, _ImageSize, 50);
            returnedCaregiver.IdentityCardPhotoFaceData = await fronPhotoTask;
            returnedCaregiver.IdentityCardPhotoBackData = await backPhotoTask;
            _CurrentCaregiver = returnedCaregiver;
            return returnedCaregiver;
        }

        public async Task<bool> SaveImage(string url, Image image)
        {
            var ret = await _apiClient.SetImage(url, image);
            return ret;
        }

        public async Task<bool> Save()
        {
            return await Save(_CurrentCaregiver);
        }
    }
}