using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Guarantor
{
    public class GuarantorsViewModel
    {
        public ObservableCollection<GuarantorModel> Guarantors { get; set; }
        private IList<OrphanageDataModel.Persons.Guarantor> _SourceGuarantor;

        private readonly IApiClient _apiClient;
        private readonly IMapperService _mapperService;
        private readonly ITranslateService _translateService;
        private readonly IDataFormatterService _dataFormatterService;
        private readonly IExceptionHandler _exceptionHandler;

        public event EventHandler DataLoaded;

        public GuarantorsViewModel(IApiClient apiClient, IMapperService mapperService,
            ITranslateService translateService, IDataFormatterService dataFormatterService, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _mapperService = mapperService;
            _translateService = translateService;
            _dataFormatterService = dataFormatterService;
            _exceptionHandler = exceptionHandler;
        }

        public async void LoadGuarantors()
        {
            var guarantorsCount = await _apiClient.GuarantorsController_GetGuarantorsCountAsync();
            var ReturnedGuarantors = await _apiClient.GuarantorsController_GetAllAsync(guarantorsCount, 0);

            _SourceGuarantor = ReturnedGuarantors;

            Guarantors = new ObservableCollection<GuarantorModel>(_mapperService.MapToGuarantorModel(_SourceGuarantor));

            UpdateFamiliesCount();
            UpdateOrphansCount();
            UpdateBailsCount();

            DataLoaded?.Invoke(this, new EventArgs());
        }

        private void UpdateFamiliesCount()
        {
            new Thread(new ThreadStart(async () =>
            {
                foreach (var guarantor in Guarantors)
                {
                    var value = await _apiClient.GuarantorsController_GetFamiliesCountAsync(guarantor.Id);
                    guarantor.FamiliesCount = value;
                }
            })).Start();
        }

        private void UpdateBailsCount()
        {
            new Thread(new ThreadStart(async () =>
            {
                foreach (var guarantor in Guarantors)
                {
                    var value = await _apiClient.GuarantorsController_GetBailsCountAsync(guarantor.Id);
                    guarantor.BailsCount = value;
                }
            })).Start();
        }

        private void UpdateOrphansCount()
        {
            new Thread(new ThreadStart(async () =>
            {
                foreach (var guarantor in Guarantors)
                {
                    var value = await _apiClient.GuarantorsController_GetOrphansCountAsync(guarantor.Id);
                    guarantor.OrphansCount = value;
                }
            })).Start();
        }

        public async void Update(int guarantorId)
        {
            var sourceGuarantor = await _apiClient.GuarantorsController_GetAsync(guarantorId);

            var sourceGuarantorToReplace = _SourceGuarantor.FirstOrDefault(b => b.Id == guarantorId);
            var sourceGuarantorIndex = _SourceGuarantor.IndexOf(sourceGuarantorToReplace);
            _SourceGuarantor[sourceGuarantorIndex] = sourceGuarantor;

            var guarantorModel = _mapperService.MapToGuarantorModel(sourceGuarantor);

            var guarantorToEdit = Guarantors.FirstOrDefault(c => c.Id == guarantorId);
            var guarantorToEditIndex = Guarantors.IndexOf(guarantorToEdit);
            Guarantors[guarantorToEditIndex] = guarantorModel;
        }

        public async Task<long?> SetColor(int caregiverId, long? colorValue)
        {
            long? returnedColor = null;
            try
            {
                var guarantor = _SourceGuarantor.FirstOrDefault(c => c.Id == caregiverId);
                returnedColor = guarantor.ColorMark;
                if (colorValue != Color.White.ToArgb() && colorValue != Color.Black.ToArgb())
                    guarantor.ColorMark = colorValue;
                else
                    guarantor.ColorMark = -1;
                await _apiClient.GuarantorsController_SetGuarantorColorAsync(guarantor.Id, (int)guarantor.ColorMark.Value);
                return guarantor.ColorMark;
            }
            catch (ApiClientException apiEx)
            {
                _exceptionHandler.HandleApiSaveException(apiEx);
                return returnedColor;
            }
        }

        public async Task<bool> Delete(int guarantorId, bool ForceDelete)
        {
            try
            {
                var guarantor = _SourceGuarantor.FirstOrDefault(c => c.Id == guarantorId);
                if (guarantor == null)
                    return false;
                await _apiClient.GuarantorsController_DeleteAsync(guarantorId, ForceDelete);
                return true;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }

        public async Task<IEnumerable<int>> BailsIds(int guarantorId)
        {
            return await _apiClient.GuarantorsController_GetBailsIdsAsync(guarantorId);
        }

        public async Task<IList<OrphanageDataModel.FinancialData.Bail>> Bails(int guarantorId)
        {
            return await _apiClient.GuarantorsController_GetBailsAsync(guarantorId);
        }

        public async Task<IEnumerable<int>> OrphansIds(int guarantorId)
        {
            return await _apiClient.GuarantorsController_GetOrphansIdsAsync(guarantorId);
        }

        public async Task<IList<OrphanageDataModel.Persons.Orphan>> Orphans(int guarantorId)
        {
            return await _apiClient.GuarantorsController_GetOrphansAsync(guarantorId);
        }

        public async Task<IEnumerable<int>> FamiliesIds(int guarantorId)
        {
            return await _apiClient.GuarantorsController_GetFamiliesIdsAsync(guarantorId);
        }

        public async Task<IList<OrphanageDataModel.RegularData.Family>> Families(int guarantorId)
        {
            return await _apiClient.GuarantorsController_GetFamiliesAsync(guarantorId);
        }

        public async Task<IList<int>> FamiliesIds(IEnumerable<int> guarantorIds)
        {
            if (guarantorIds == null || guarantorIds.Count() == 0) return null;
            IList<int> returnedIds = new List<int>();
            foreach (var guarantorId in guarantorIds)
            {
                var familiesIds = await FamiliesIds(guarantorId);
                if (familiesIds != null && familiesIds.Count() > 0)
                {
                    foreach (var familyId in familiesIds)
                    {
                        returnedIds.Add(familyId);
                    }
                }
            }
            return returnedIds;
        }

        public async Task<IList<int>> BailsIds(IEnumerable<int> guarantorIds)
        {
            if (guarantorIds == null || guarantorIds.Count() == 0) return null;
            IList<int> returnedIds = new List<int>();
            foreach (var guarantorId in guarantorIds)
            {
                var bailsIds = await BailsIds(guarantorId);
                if (bailsIds != null && bailsIds.Count() > 0)
                {
                    foreach (var bailId in bailsIds)
                    {
                        returnedIds.Add(bailId);
                    }
                }
            }
            return returnedIds;
        }

        public async Task<IList<int>> OrphansIds(IEnumerable<int> guarantorIds)
        {
            if (guarantorIds == null || guarantorIds.Count() == 0) return null;
            IList<int> returnedIds = new List<int>();
            foreach (var id in guarantorIds)
            {
                var orphansIds = await OrphansIds(id);
                if (orphansIds != null && orphansIds.Count() > 0)
                {
                    foreach (var orphanId in orphansIds)
                    {
                        returnedIds.Add(orphanId);
                    }
                }
            }
            return returnedIds;
        }

        public OrphanageDataModel.Persons.Guarantor GetSourceGuarantor(int guarantorModelId) =>
            _SourceGuarantor.FirstOrDefault(c => c.Id == guarantorModelId);
    }
}