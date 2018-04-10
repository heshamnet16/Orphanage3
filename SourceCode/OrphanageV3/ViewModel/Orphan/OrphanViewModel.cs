using OrphanageV3.Services;
using System.Drawing;
using System.Threading.Tasks;
using Unity;

namespace OrphanageV3.ViewModel.Orphan
{
    public class OrphanViewModel
    {
        private OrphansViewModel _orphansViewModel = Program.Factory.Resolve<OrphansViewModel>();

        private readonly IApiClient _apiClient;
        private Size _ImageSize = new Size(153, 126);

        public OrphanageDataModel.Persons.Orphan CurrentOrphan { get; private set; }

        public Size ImagesSize { get => _ImageSize; set { _ImageSize = value; } }

        public OrphanViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<bool> Save(OrphanageDataModel.Persons.Orphan orphan)
        {
            orphan.BirthCertificatePhotoData = null;
            orphan.FacePhotoData = null;
            orphan.FamilyCardPagePhotoData = null;
            orphan.FullPhotoData = null;
            if (orphan.Education != null)
            {
                orphan.Education.CertificatePhotoBack = null;
                orphan.Education.CertificatePhotoFront = null;
            }
            if (orphan.HealthStatus != null)
                orphan.HealthStatus.ReporteFileData = null;
            await _apiClient.OrphansController_PutAsync(orphan);
            return true;
        }

        public async Task<OrphanageDataModel.Persons.Orphan> Add(OrphanageDataModel.Persons.Orphan orphan)
        {
            if (orphan == null) return null;
            orphan.BirthCertificatePhotoData = null;
            orphan.FacePhotoData = null;
            orphan.FamilyCardPagePhotoData = null;
            orphan.FullPhotoData = null;
            if (orphan.Education != null)
            {
                orphan.Education.CertificatePhotoBack = null;
                orphan.Education.CertificatePhotoFront = null;
            }
            if (orphan.HealthStatus != null)
                orphan.HealthStatus.ReporteFileData = null;
            try
            {
                var retOrp = (OrphanageDataModel.Persons.Orphan)await _apiClient.OrphansController_PostAsync(orphan);
            }
            catch (ApiClientException apiEx)
            {
                //Created
                if (apiEx.StatusCode == "201")
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<OrphanageDataModel.Persons.Orphan>(apiEx.Response) ?? null;
                return null;
            }
            return null;
        }

        public async Task<OrphanageDataModel.Persons.Orphan> getOrphan(int Oid)
        {
            var returnedOrphan = await _apiClient.OrphansController_GetAsync(Oid);
            var facePhotoTask = _apiClient.GetImageData(returnedOrphan.FacePhotoURI, _ImageSize, 50);
            var bodyPhotoTask = _apiClient.GetImageData(returnedOrphan.FullPhotoURI, _ImageSize, 50);
            var birthCertificateTask = _apiClient.GetImageData(returnedOrphan.BirthCertificatePhotoURI, _ImageSize, 50);
            var familiyCardPhotoTask = _apiClient.GetImageData(returnedOrphan.FamilyCardPagePhotoURI, _ImageSize, 50);
            if (returnedOrphan.EducationId.HasValue)
            {
                try
                {
                    returnedOrphan.Education.CertificatePhotoFront = await _apiClient.GetImageData(returnedOrphan.Education.CertificateImageURI, _ImageSize, 50);
                }
                catch (ApiClientException apiException)
                {
                    if (apiException.StatusCode != "404")
                    {
                        //TODO show error message
                    }
                    returnedOrphan.Education.CertificatePhotoFront = null;
                }
                try
                {
                    returnedOrphan.Education.CertificatePhotoBack = await _apiClient.GetImageData(returnedOrphan.Education.CertificateImage2URI, _ImageSize, 50);
                }
                catch (ApiClientException apiException)
                {
                    if (apiException.StatusCode != "404")
                    {
                        //TODO show error message
                    }
                    returnedOrphan.Education.CertificatePhotoBack = null;
                }
            }
            if (returnedOrphan.HealthId.HasValue)
            {
                try
                {
                    returnedOrphan.HealthStatus.ReporteFileData = await _apiClient.GetImageData(returnedOrphan.HealthStatus.ReporteFileURI);
                }
                catch (ApiClientException apiException)
                {
                    if (apiException.StatusCode != "404")
                    {
                        //TODO show error message
                    }
                    returnedOrphan.HealthStatus.ReporteFileData = null;
                }
            }
            returnedOrphan.FullPhotoData = await bodyPhotoTask;
            returnedOrphan.FacePhotoData = await facePhotoTask;
            returnedOrphan.BirthCertificatePhotoData = await birthCertificateTask;
            returnedOrphan.FamilyCardPagePhotoData = await familiyCardPhotoTask;
            CurrentOrphan = returnedOrphan;
            return returnedOrphan;
        }

        public async Task<bool> SaveImage(string url, Image image)
        {
            var ret = await _apiClient.SetImage(url, image);
            return ret;
        }

        public async Task<bool> Save()
        {
            return await Save(CurrentOrphan);
        }
    }
}