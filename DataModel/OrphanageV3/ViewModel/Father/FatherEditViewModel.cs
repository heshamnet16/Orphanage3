using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Father
{
    public class FatherEditViewModel
    {
        private readonly IApiClient _apiClient;
        private Size _ImageSize = new Size(153, 126);
        public Size ImagesSize { get => _ImageSize; set { _ImageSize = value; } }

        private OrphanageDataModel.Persons.Father _CurrentFather = null;

        public FatherEditViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<bool> Save(OrphanageDataModel.Persons.Father father)
        {
            try
            {
                father.DeathCertificatePhotoData = null;
                father.PhotoData = null;
                await _apiClient.FathersController_PutAsync(father);
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

        public async Task<OrphanageDataModel.Persons.Father> getFather(int Cid)
        {
            var returnedFather = await _apiClient.FathersController_GetAsync(Cid);
            var deathCertificatePhotoTask = _apiClient.GetImageData(returnedFather.DeathCertificateImageURI, _ImageSize, 50);
            var personalPhotoTask = _apiClient.GetImageData(returnedFather.PersonalPhotoURI, _ImageSize, 50);
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
