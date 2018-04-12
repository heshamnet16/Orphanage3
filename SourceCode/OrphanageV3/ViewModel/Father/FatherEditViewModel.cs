using OrphanageV3.Services;
using System.Drawing;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Father
{
    public class FatherEditViewModel
    {
        private readonly IApiClient _apiClient;

        private OrphanageDataModel.Persons.Father _CurrentFather = null;

        public FatherEditViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<bool> Save(OrphanageDataModel.Persons.Father father)
        {
            father.DeathCertificatePhotoData = null;
            father.PhotoData = null;
            await _apiClient.FathersController_PutAsync(father);
            return true;
        }

        public async Task<OrphanageDataModel.Persons.Father> getFather(int Cid)
        {
            var returnedFather = await _apiClient.FathersController_GetAsync(Cid);
            var deathCertificatePhotoTask = _apiClient.GetImageData(returnedFather.DeathCertificateImageURI);
            var personalPhotoTask = _apiClient.GetImageData(returnedFather.PersonalPhotoURI);
            returnedFather.DeathCertificatePhotoData = await deathCertificatePhotoTask;
            returnedFather.PhotoData = await personalPhotoTask;
            _CurrentFather = returnedFather;
            return returnedFather;
        }

        public async Task<bool> SaveImage(string url, Image image)
        {
            var ret = await _apiClient.SetImage(url, image);
            return ret;
        }

        public async Task<bool> Save()
        {
            return await Save(_CurrentFather);
        }
    }
}