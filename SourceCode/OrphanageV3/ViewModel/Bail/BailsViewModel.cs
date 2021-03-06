﻿using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Bail
{
    public class BailsViewModel
    {
        public ObservableCollection<BailModel> Bails { get; set; }
        private IList<OrphanageDataModel.FinancialData.Bail> _SourceBails;

        private readonly IApiClient _apiClient;
        private readonly IMapperService _mapperService;
        private readonly ITranslateService _translateService;
        private readonly IDataFormatterService _dataFormatterService;
        private readonly IExceptionHandler _exceptionHandler;

        public event EventHandler DataLoaded;

        public BailsViewModel(IApiClient apiClient, IMapperService mapperService,
            ITranslateService translateService, IDataFormatterService dataFormatterService, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _mapperService = mapperService;
            _translateService = translateService;
            _dataFormatterService = dataFormatterService;
            _exceptionHandler = exceptionHandler;
        }

        public async void LoadBails()
        {
            var bailsCount = await _apiClient.Bails_GetBailsCountAsync();
            var ReturnedBails = await _apiClient.Bails_GetAllAsync(bailsCount, 0);

            _SourceBails = ReturnedBails;

            Bails = new ObservableCollection<BailModel>(_mapperService.MapToBailModel(_SourceBails));

            DataLoaded?.Invoke(this, new EventArgs());
        }

        public async void LoadBails(IEnumerable<int> bailsIds)
        {
            var ReturnedBails = await _apiClient.Bails_GetByIdsAsync(bailsIds);

            _SourceBails = ReturnedBails;

            Bails = new ObservableCollection<BailModel>(_mapperService.MapToBailModel(_SourceBails));

            DataLoaded?.Invoke(this, new EventArgs());
        }

        public async void LoadBailsByIsFamily(bool value)
        {
            var ReturnedBails = await _apiClient.Bails_GetBailsByFamilyAsync(value);

            _SourceBails = ReturnedBails;

            Bails = new ObservableCollection<BailModel>(_mapperService.MapToBailModel(_SourceBails));

            DataLoaded?.Invoke(this, new EventArgs());
        }

        public async void Update(int bailId)
        {
            var sourceBail = await _apiClient.Bails_GetAsync(bailId);

            var sourceBailToReplace = _SourceBails.FirstOrDefault(b => b.Id == bailId);
            var sourceBailIndex = _SourceBails.IndexOf(sourceBailToReplace);
            _SourceBails[sourceBailIndex] = sourceBail;

            var bailModel = _mapperService.MapToBailModel(sourceBail);
            bailModel.OrphansCount = await _apiClient.Bails_GetOrphansCountAsync(bailId);
            bailModel.FamiliesCount = await _apiClient.Bails_GetFamiliesCountAsync(bailId);

            var bailToEdit = Bails.FirstOrDefault(c => c.Id == bailId);
            var bailToEditIndex = Bails.IndexOf(bailToEdit);
            Bails[bailToEditIndex] = bailModel;
        }

        public async Task<bool> Delete(int bailId, bool ForceDelete)
        {
            var bail = _SourceBails.FirstOrDefault(c => c.Id == bailId);
            if (bail == null)
                return false;
            try
            {
                await _apiClient.Bails_DeleteAsync(bailId, ForceDelete);
                return true;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
        }

        public async Task<IEnumerable<int>> OrphansIds(int bailId)
        {
            return await _apiClient.Bails_GetOrphansIdsAsync(bailId);
        }

        public async Task<IList<OrphanageDataModel.Persons.Orphan>> Orphans(int bailId)
        {
            return await _apiClient.Bails_GetOrphansAsync(bailId);
        }

        public async Task<IEnumerable<int>> FamiliesIds(int bailId)
        {
            return await _apiClient.Bails_GetFamiliesIdsAsync(bailId);
        }

        public async Task<IList<OrphanageDataModel.RegularData.Family>> Families(int bailId)
        {
            return await _apiClient.Bails_GetFamiliesAsync(bailId);
        }

        public async Task<IList<int>> FamiliesIds(IEnumerable<int> bailsIds)
        {
            if (bailsIds == null || bailsIds.Count() == 0) return null;
            IList<int> returnedIds = new List<int>();
            foreach (var bailId in bailsIds)
            {
                var familiesIds = await FamiliesIds(bailId);
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

        public async Task<IList<int>> OrphansIds(IEnumerable<int> bailsIds)
        {
            if (bailsIds == null || bailsIds.Count() == 0) return null;
            IList<int> returnedIds = new List<int>();
            foreach (var id in bailsIds)
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

        public OrphanageDataModel.FinancialData.Bail GetSourceBail(int bailModelId) =>
            _SourceBails.FirstOrDefault(c => c.Id == bailModelId);
    }
}