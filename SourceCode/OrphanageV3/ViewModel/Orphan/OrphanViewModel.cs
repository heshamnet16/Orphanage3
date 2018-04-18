using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System.Drawing;
using System.Threading.Tasks;
using Unity;

namespace OrphanageV3.ViewModel.Orphan
{
    public class OrphanViewModel
    {
        private OrphansViewModel _orphansViewModel = Program.Factory.Resolve<OrphansViewModel>();

        private readonly IApiClient _apiClient;
        private readonly Main.MainViewModel _mainViewModel;
        private readonly IExceptionHandler _exceptionHandler;

        //private Size _ImageSize = new Size(153, 126);

        public OrphanageDataModel.Persons.Orphan CurrentOrphan { get; private set; }

        //public Size ImagesSize { get => _ImageSize; set { _ImageSize = value; } }

        public OrphanViewModel(IApiClient apiClient, Main.MainViewModel mainViewModel, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _mainViewModel = mainViewModel;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<bool> Save(OrphanageDataModel.Persons.Orphan orphan)
        {
            try
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
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
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
                var retObject = await _exceptionHandler.HandleApiPostFunctionsAndShowErrors(getOrphan, apiEx);
                if (retObject == null)
                {
                    retObject = await _exceptionHandler.HandleApiPostFunctions(getOrphan, apiEx);
                    if (retObject != null)
                    { _mainViewModel.ShowOrphanEditView(retObject.Id); }
                    return null;
                }
                else
                    return retObject;
            }
            return null;
        }

        public async Task<OrphanageDataModel.Persons.Orphan> getOrphan(int Oid)
        {
            var returnedOrphan = await _apiClient.OrphansController_GetAsync(Oid);
            var facePhotoTask = _apiClient.GetImageData(returnedOrphan.FacePhotoURI);
            var bodyPhotoTask = _apiClient.GetImageData(returnedOrphan.FullPhotoURI);
            var birthCertificateTask = _apiClient.GetImageData(returnedOrphan.BirthCertificatePhotoURI);
            var familiyCardPhotoTask = _apiClient.GetImageData(returnedOrphan.FamilyCardPagePhotoURI);
            if (returnedOrphan.EducationId.HasValue)
            {
                try
                {
                    returnedOrphan.Education.CertificatePhotoFront = await _apiClient.GetImageData(returnedOrphan.Education.CertificateImageURI);
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
                    returnedOrphan.Education.CertificatePhotoBack = await _apiClient.GetImageData(returnedOrphan.Education.CertificateImage2URI);
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
            return await Save(CurrentOrphan);
        }
    }
}