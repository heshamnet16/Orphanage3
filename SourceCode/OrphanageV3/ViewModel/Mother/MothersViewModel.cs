using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Mother
{
    public class MothersViewModel
    {
        public ObservableCollection<MotherModel> Mothers { get; set; }
        private IList<OrphanageDataModel.Persons.Mother> _SourceMothers;

        private readonly IApiClient _apiClient;
        private readonly IMapperService _mapperService;
        private readonly ITranslateService _translateService;
        private readonly IDataFormatterService _dataFormatterService;
        private readonly IExceptionHandler _exceptionHandler;

        public event EventHandler DataLoaded;

        public MothersViewModel(IApiClient apiClient, IMapperService mapperService,
            ITranslateService translateService, IDataFormatterService dataFormatterService, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _mapperService = mapperService;
            _translateService = translateService;
            _dataFormatterService = dataFormatterService;
            _exceptionHandler = exceptionHandler;
        }

        public async void LoadMothers()
        {
            var mothersCount = await _apiClient.Mothers_GetMotherCountAsync();
            var ReturnedMothers = await _apiClient.Mothers_GetAllAsync(mothersCount, 0);

            _SourceMothers = ReturnedMothers;

            Mothers = new ObservableCollection<MotherModel>(await _mapperService.MapToMotherModel(_SourceMothers));
            UpdateMotherOrphansCount();
            DataLoaded?.Invoke(this, new EventArgs());
        }

        public async void LoadMothers(IEnumerable<int> motherIdsList)
        {
            var ReturnedMothers = await _apiClient.Mothers_GetByIdsAsync(motherIdsList);

            _SourceMothers = ReturnedMothers;

            Mothers = new ObservableCollection<MotherModel>(await _mapperService.MapToMotherModel(_SourceMothers));
            UpdateMotherOrphansCount();
            DataLoaded?.Invoke(this, new EventArgs());
        }

        private void UpdateMotherOrphansCount()
        {
            new Thread(new ThreadStart(async () =>
            {
                foreach (var mother in Mothers)
                {
                    mother.OrphansCount = await _apiClient.Mothers_GetOrphansCountAsync(mother.Id);
                }
            })).Start();
        }

        public async void Update(int motherId)
        {
            var sourceMother = await _apiClient.Mothers_GetAsync(motherId);
            var orphansCountTask = _apiClient.Mothers_GetOrphansCountAsync(motherId);
            //update father object in the source list
            var sourceMotherIndex = _SourceMothers.IndexOf(_SourceMothers.FirstOrDefault(f => f.Id == motherId));
            _SourceMothers[sourceMotherIndex] = sourceMother;

            var motherModel = await _mapperService.MapToMotherModel(sourceMother);
            var MotherToEdit = Mothers.FirstOrDefault(c => c.Id == motherId);
            var motherToEditIndex = Mothers.IndexOf(MotherToEdit);
            motherModel.OrphansCount = await orphansCountTask;
            Mothers[motherToEditIndex] = motherModel;
        }

        public async Task<long?> SetColor(int motherId, long? colorValue)
        {
            long? returnedColor = null;
            try
            {
                var mother = _SourceMothers.FirstOrDefault(c => c.Id == motherId);
                returnedColor = mother.ColorMark;
                if (colorValue != Color.White.ToArgb() && colorValue != Color.Black.ToArgb())
                    mother.ColorMark = colorValue;
                else
                    mother.ColorMark = -1;
                await _apiClient.Mothers_SetMotherColorAsync(mother.Id, (int)mother.ColorMark.Value);
                return mother.ColorMark;
            }
            catch (ApiClientException apiEx)
            {
                _exceptionHandler.HandleApiSaveException(apiEx);
                return returnedColor;
            }
        }

        public async Task<IList<int>> OrphansIds(int motherId)
        {
            var mother = _SourceMothers.FirstOrDefault(c => c.Id == motherId);
            var orphansList = await _apiClient.Mothers_GetOrphansAsync(motherId);
            if (orphansList != null && orphansList.Count > 0)
                return orphansList.Select(o => o.Id).ToList();
            else
                return null;
        }

        public IList<int> FathersIds(int motherId)
        {
            var mother = _SourceMothers.FirstOrDefault(c => c.Id == motherId);
            IList<int> fatherList = new List<int>();
            foreach (var family in mother.Families)
            {
                fatherList.Add(family.FatherId);
            }
            if (fatherList != null && fatherList.Count > 0)
                return fatherList;
            else
                return null;
        }

        public IList<int> FamiliesIds(int motherId)
        {
            var mother = _SourceMothers.FirstOrDefault(c => c.Id == motherId);
            IList<int> familiesList = new List<int>();
            foreach (var family in mother.Families)
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