using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Caregiver
{
    public class CaregiversViewModel
    {
        public ObservableCollection<CaregiverModel> Caregivers { get; set; }
        private IList<Services.Caregiver> _SourceCaregivers;

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

            DataLoaded?.Invoke(this,new EventArgs());
        }
    }
}
