﻿using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
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
        private readonly IExceptionHandler _exceptionHandler;

        public event EventHandler DataLoaded;

        public CaregiversViewModel(IApiClient apiClient, IMapperService mapperService,
            ITranslateService translateService, IDataFormatterService dataFormatterService, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _mapperService = mapperService;
            _translateService = translateService;
            _dataFormatterService = dataFormatterService;
            _exceptionHandler = exceptionHandler;
        }

        public async void LoadCaregivers()
        {
            var caregiversCount = await _apiClient.Caregivers_GetCaregiversCountAsync();
            var ReturnedCaregivers = await _apiClient.Caregivers_GetAllAsync(caregiversCount, 0);

            _SourceCaregivers = ReturnedCaregivers;

            Caregivers = new ObservableCollection<CaregiverModel>(_mapperService.MapToCaregiverModel(_SourceCaregivers));

            DataLoaded?.Invoke(this, new EventArgs());
        }

        public async void Update(int caregiverId)
        {
            var sourceCaregiver = await _apiClient.Caregivers_GetAsync(caregiverId);

            var sourceCaregiverIndex = _SourceCaregivers.IndexOf(_SourceCaregivers.FirstOrDefault(c => c.Id == caregiverId));
            _SourceCaregivers[sourceCaregiverIndex] = sourceCaregiver;

            var caregiverModel = _mapperService.MapToCaregiverModel(sourceCaregiver);

            var CaregiverToEdit = Caregivers.FirstOrDefault(c => c.Id == caregiverId);
            var caregiverToEditIndex = Caregivers.IndexOf(CaregiverToEdit);
            Caregivers[caregiverToEditIndex] = caregiverModel;
        }

        public async Task<bool> Delete(int caregiverId, bool ForceDelete)
        {
            try
            {
                var caregiver = _SourceCaregivers.FirstOrDefault(c => c.Id == caregiverId);
                if (caregiver == null)
                    return false;
                if (caregiver.Orphans != null && caregiver.Orphans.Count > 0)
                {
                    if (ForceDelete)
                    {
                        var orphans = await _apiClient.Caregivers_GetFamilyOrphansAsync(caregiverId);
                        foreach (var orphan in orphans)
                            await _apiClient.Orphans_DeleteAsync(orphan.Id);
                    }
                    else
                    {
                        // the caregiver has orphans
                        return false;
                    }
                }
                await _apiClient.Caregivers_DeleteAsync(caregiverId);
                return true;
            }
            catch (ApiClientException apiEx)
            {
                return _exceptionHandler.HandleApiSaveException(apiEx);
            }
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
                    caregiver.ColorMark = -1;
                await _apiClient.Caregivers_SetCaregiverColorAsync(caregiver.Id, (int)caregiver.ColorMark.Value);
                return caregiver.ColorMark;
            }
            catch (ApiClientException apiEx)
            {
                _exceptionHandler.HandleApiSaveException(apiEx);
                return returnedColor;
            }
        }

        public IList<int> OrphansIds(int caregiverId)
        {
            var caregiver = _SourceCaregivers.FirstOrDefault(c => c.Id == caregiverId);
            return caregiver.Orphans.Select(o => o.Id).ToList();
        }

        public async Task<IList<OrphanageDataModel.Persons.Orphan>> Orphans(int caregiverId)
        {
            return await _apiClient.Caregivers_GetFamilyOrphansAsync(caregiverId);
        }

        public async Task<IList<int>> MothersIds(IEnumerable<int> caregiversIds)
        {
            if (caregiversIds == null || caregiversIds.Count() == 0) return null;
            IList<int> returnedIds = new List<int>();
            foreach (var id in caregiversIds)
            {
                var orphans = await Orphans(id);
                if (orphans != null && orphans.Count > 0)
                {
                    foreach (var orphan in orphans)
                    {
                        returnedIds.Add(orphan.Family.MotherId);
                    }
                }
            }
            return returnedIds;
        }

        public async Task<IList<int>> FathersIds(IEnumerable<int> caregiversIds)
        {
            if (caregiversIds == null || caregiversIds.Count() == 0) return null;
            IList<int> returnedIds = new List<int>();
            foreach (var id in caregiversIds)
            {
                var orphans = await Orphans(id);
                if (orphans != null && orphans.Count > 0)
                {
                    foreach (var orphan in orphans)
                    {
                        returnedIds.Add(orphan.Family.FatherId);
                    }
                }
            }
            return returnedIds;
        }

        public async Task<IList<int>> FamiliesIds(IEnumerable<int> caregiversIds)
        {
            if (caregiversIds == null || caregiversIds.Count() == 0) return null;
            IList<int> returnedIds = new List<int>();
            foreach (var id in caregiversIds)
            {
                var orphans = await Orphans(id);
                if (orphans != null && orphans.Count > 0)
                {
                    foreach (var orphan in orphans)
                    {
                        returnedIds.Add(orphan.Family.Id);
                    }
                }
            }
            return returnedIds;
        }

        public OrphanageDataModel.Persons.Caregiver GetSourceCaregiver(int caregiverModelId) =>
            _SourceCaregivers.FirstOrDefault(c => c.Id == caregiverModelId);
    }
}