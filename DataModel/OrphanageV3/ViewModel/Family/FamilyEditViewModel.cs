using OrphanageV3.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Family
{
    public class FamilyEditViewModel
    {
        private readonly IApiClient _apiClient;
        private Size _ImageSize = new Size(153, 126);
        public Size ImagesSize { get => _ImageSize; set { _ImageSize = value; } }

        private OrphanageDataModel.RegularData.Family _CurrentFamily = null;

        public FamilyEditViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<bool> Save(OrphanageDataModel.RegularData.Family family)
        {

            family.FamilyCardImagePage1Data = null;
            family.FamilyCardImagePage2Data= null;
            await _apiClient.FamiliesController_PutAsync(family);
            return true;

        }

        public async Task<OrphanageDataModel.RegularData.Family> getFamily(int Cid)
        {
            var returnedFamily = await _apiClient.FamiliesController_GetAsync(Cid);
            var FamilyCardP1Task = _apiClient.GetImageData(returnedFamily.FamilyCardImagePage1URI, _ImageSize, 50);
            var FamilyCardP2Task = _apiClient.GetImageData(returnedFamily.FamilyCardImagePage2URI, _ImageSize, 50);
            returnedFamily.FamilyCardImagePage1Data = await FamilyCardP1Task;
            returnedFamily.FamilyCardImagePage2Data = await FamilyCardP2Task;
            _CurrentFamily = returnedFamily;
            return returnedFamily;
        }

        public async Task<bool> SaveImage(string url, Image image)
        {
            var ret = await _apiClient.SetImage(url, image);
            return ret;
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
                    var fam = (OrphanageDataModel.RegularData.Family)await _apiClient.FamiliesController_PostAsync(family);
                    return fam ?? null;
                }
                catch (ApiClientException apiEx)
                {
                    //Created
                    if (apiEx.StatusCode == "201")
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<OrphanageDataModel.RegularData.Family>(apiEx.Response) ?? null;
                    return null;
                }
            }
            return null;
        }
    }
}
