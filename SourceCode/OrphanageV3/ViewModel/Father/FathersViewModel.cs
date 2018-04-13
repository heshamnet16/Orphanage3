using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Father
{
    public class FathersViewModel
    {
        public ObservableCollection<FatherModel> Fathers { get; set; }
        private IList<OrphanageDataModel.Persons.Father> _SourceFathers;

        private readonly IApiClient _apiClient;
        private readonly IMapperService _mapperService;
        private readonly ITranslateService _translateService;
        private readonly IDataFormatterService _dataFormatterService;
        private readonly IExceptionHandler _exceptionHandler;

        public event EventHandler DataLoaded;

        public FathersViewModel(IApiClient apiClient, IMapperService mapperService,
            ITranslateService translateService, IDataFormatterService dataFormatterService, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _mapperService = mapperService;
            _translateService = translateService;
            _dataFormatterService = dataFormatterService;
            _exceptionHandler = exceptionHandler;
        }

        public async void LoadFathers()
        {
            var fathersCount = await _apiClient.FathersController_GetFathersCountAsync();
            var ReturnedFathers = await _apiClient.FathersController_GetAllAsync(fathersCount, 0);

            _SourceFathers = ReturnedFathers;

            Fathers = new ObservableCollection<FatherModel>(await _mapperService.MapToFatherModel(_SourceFathers));
            UpdateFathersOrphansCount();
            DataLoaded?.Invoke(this, new EventArgs());
        }

        public async void LoadFathers(IEnumerable<int> fathersIdsList)
        {
            var ReturnedFathers = await _apiClient.FathersController_GetByIdsAsync(fathersIdsList);

            _SourceFathers = ReturnedFathers;

            Fathers = new ObservableCollection<FatherModel>(await _mapperService.MapToFatherModel(_SourceFathers));
            UpdateFathersOrphansCount();
            DataLoaded?.Invoke(this, new EventArgs());
        }

        private void UpdateFathersOrphansCount()
        {
            new Thread(new ThreadStart(async () =>
            {
                foreach (var mother in Fathers)
                {
                    mother.OrphansCount = await _apiClient.MothersController_GetOrphansCountAsync(mother.Id);
                }
            })).Start();
        }

        public async void Update(int fatherId)
        {
            var sourceFather = await _apiClient.FathersController_GetAsync(fatherId);
            var orphansCountTask = _apiClient.FathersController_GetOrphansCountAsync(fatherId);
            //update father object in the source list
            var sourceFatherIndex = _SourceFathers.IndexOf(_SourceFathers.FirstOrDefault(f => f.Id == fatherId));
            _SourceFathers[sourceFatherIndex] = sourceFather;

            var fatherModel = await _mapperService.MapToFatherModel(sourceFather);
            var FatherToEdit = Fathers.FirstOrDefault(c => c.Id == fatherId);
            var fatherToEditIndex = Fathers.IndexOf(FatherToEdit);
            fatherModel.OrphansCount = await orphansCountTask;
            Fathers[fatherToEditIndex] = fatherModel;
        }

        public async Task<long?> SetColor(int motherId, long? colorValue)
        {
            long? returnedColor = null;
            try
            {
                var father = _SourceFathers.FirstOrDefault(c => c.Id == motherId);
                returnedColor = father.ColorMark;
                if (colorValue != Color.White.ToArgb() && colorValue != Color.Black.ToArgb())
                    father.ColorMark = colorValue;
                else
                    father.ColorMark = -1;
                await _apiClient.FathersController_SetFatherColorAsync(father.Id, (int)father.ColorMark.Value);
                return father.ColorMark;
            }
            catch (ApiClientException apiEx)
            {
                _exceptionHandler.HandleApiSaveException(apiEx);
                return returnedColor;
            }
        }

        public async Task<IList<int>> OrphansIds(int motherId)
        {
            var father = _SourceFathers.FirstOrDefault(c => c.Id == motherId);
            var orphansList = await _apiClient.FathersController_GetOrphansAsync(motherId);
            if (orphansList != null && orphansList.Count > 0)
                return orphansList.Select(o => o.Id).ToList();
            else
                return null;
        }

        public IList<int> MothersIds(int fatherId)
        {
            var father = _SourceFathers.FirstOrDefault(c => c.Id == fatherId);
            IList<int> motherList = new List<int>();
            foreach (var family in father.Families)
            {
                motherList.Add(family.MotherId);
            }
            if (motherList != null && motherList.Count > 0)
                return motherList;
            else
                return null;
        }

        public IList<int> FamiliesIds(int fatherId)
        {
            var father = _SourceFathers.FirstOrDefault(c => c.Id == fatherId);
            IList<int> familiesList = new List<int>();
            foreach (var family in father.Families)
            {
                familiesList.Add(family.Id);
            }
            if (familiesList != null && familiesList.Count > 0)
                return familiesList;
            else
                return null;
        }
    }
}