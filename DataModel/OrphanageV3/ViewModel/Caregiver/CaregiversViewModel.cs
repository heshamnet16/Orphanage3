using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Caregiver
{
    public class CaregiversViewModel
    {
        public ObservableCollection<CaregiverModel> Caregivers { get; set; }
        private IList<OrphanageDataModel.Persons.Caregiver> _SourceCaregivers;

        private readonly IApiClient _apiClient;
        private readonly IMapperService _mapperService;
        private readonly ITranslateService _translateService;
        private readonly IDataFormatterService _dataFormatterService;

        public event EventHandler DataLoaded;

        public CaregiversViewModel(IApiClient apiClient, IMapperService mapperService,
            ITranslateService translateService, IDataFormatterService dataFormatterService)
        {
            _apiClient = apiClient;
            _mapperService = mapperService;
            _translateService = translateService;
            _dataFormatterService = dataFormatterService;
        }

        public async void LoadCaregivers()
        {
            var caregiversCount = await _apiClient.CaregiversController_GetCaregiversCountAsync();
            var ReturnedCaregivers = await _apiClient.CaregiversController_GetAllAsync(caregiversCount, 0);

            _SourceCaregivers = ReturnedCaregivers;

            Caregivers = new ObservableCollection<CaregiverModel>(_mapperService.MapToCaregiverModel(_SourceCaregivers));

            DataLoaded?.Invoke(this, new EventArgs());
        }

        public async void Update(int caregiverId)
        {
            var sourceCaregiver = await _apiClient.CaregiversController_GetAsync(caregiverId);
            var caregiverModel = _mapperService.MapToCaregiverModel(sourceCaregiver);
            var CaregiverToEdit = Caregivers.FirstOrDefault(c => c.Id == caregiverId);
            var caregiverToEditIndex = Caregivers.IndexOf(CaregiverToEdit);
            Caregivers[caregiverToEditIndex] = caregiverModel;
        }

        public async Task<bool> Delete(int caregiverId, bool ForceDelete)
        {
            var caregiver = _SourceCaregivers.FirstOrDefault(c => c.Id == caregiverId);
            if (caregiver == null)
                return false;
            if (caregiver.Orphans != null && caregiver.Orphans.Count > 0)
            {
                if (ForceDelete)
                {
                    var orphans = await _apiClient.CaregiversController_GetFamilyOrphansAsync(caregiverId);
                    foreach (var orphan in orphans)
                        await _apiClient.OrphansController_DeleteAsync(orphan.Id);
                }
                else
                {
                    // the caregiver has orphans
                    return false;
                }
            }
            await _apiClient.CaregiversController_DeleteAsync(caregiverId);
            return true;
        }

        public async Task<long?> SetColor(int caregiverId, long? colorValue)
        {
            long? returnedColor = null;
            try
            {
                var caregiver = _SourceCaregivers.FirstOrDefault(c => c.Id == caregiverId);
                returnedColor = caregiver.ColorMark;
                if (colorValue != Color.White.ToArgb() && colorValue != Color.Black.ToArgb())
                    caregiver.ColorMark = colorValue;
                else
                    caregiver.ColorMark = null;
                await _apiClient.CaregiversController_PutAsync(caregiver);
                return caregiver.ColorMark;
            }
            catch (ApiClientException apiEx)
            {
                if (apiEx.StatusCode == "304")
                    return returnedColor;
                return null;
            }
        }
        public IList<int> OrphansIds(int caregiverId)
        {
                var caregiver = _SourceCaregivers.FirstOrDefault(c => c.Id == caregiverId);
                return caregiver.Orphans.Select(o=>o.Id).ToList();
        }
    }
}
