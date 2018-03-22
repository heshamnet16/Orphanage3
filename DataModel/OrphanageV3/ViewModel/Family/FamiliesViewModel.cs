using OrphanageV3.Extensions;
using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Family
{
    public class FamiliesViewModel
    {
        public ObservableCollection<FamilyModel> Families { get; set; }
        private IList<OrphanageDataModel.RegularData.Family> _SourceFamilies;

        private readonly IApiClient _apiClient;
        private readonly IMapperService _mapperService;
        private readonly ITranslateService _translateService;
        private readonly IDataFormatterService _dataFormatterService;

        public event EventHandler DataLoaded;

        public object LockObject = new object();

        public FamiliesViewModel(IApiClient apiClient, IMapperService mapperService,
            ITranslateService translateService, IDataFormatterService dataFormatterService)
        {
            _apiClient = apiClient;
            _mapperService = mapperService;
            _translateService = translateService;
            _dataFormatterService = dataFormatterService;
        }

        public async void LoadFamilies()
        {
            var familiesCount = await _apiClient.FamiliesController_GetFamiliesCountAsync();
            var ReturnedFamilies = await _apiClient.FamiliesController_GetAllAsync(familiesCount, 0);

            _SourceFamilies = ReturnedFamilies;

            Families = new ObservableCollection<FamilyModel>(_mapperService.MapToFamilyModel(_SourceFamilies));
            UpdateFamilyOrphansCount();
            UpdateFamilyBails();
            DataLoaded?.Invoke(this, new EventArgs());
        }

        public async void LoadFamilies(IEnumerable<int> familiesIdsList)
        {
            var ReturnedFamilies = await _apiClient.FamiliesController_GetByIdsAsync(familiesIdsList);

            _SourceFamilies = ReturnedFamilies;

            Families = new ObservableCollection<FamilyModel>(_mapperService.MapToFamilyModel(_SourceFamilies));
            UpdateFamilyOrphansCount();
            UpdateFamilyBails();
            DataLoaded?.Invoke(this, new EventArgs());
        }

        private void UpdateFamilyOrphansCount()
        {
            new Thread(new ThreadStart(async () =>
            {
                foreach (var fam in Families)
                {
                    var value = await _apiClient.FamiliesController_GetOrphansCountAsync(fam.Id);
                    fam.OrphansCount = value;
                }
            })).Start();
        }

        private void UpdateFamilyBails()
        {
            new Thread(new ThreadStart(async () =>
            {
                foreach (var sourceFam in _SourceFamilies)
                {
                    if (sourceFam.BailId.HasValue)
                    {
                        var bail = await _apiClient.BailsController_GetAsync(sourceFam.BailId.Value);
                        var fam = Families.FirstOrDefault(f => f.Id == sourceFam.Id);
                        if (fam != null)
                        {
                            fam.BailAmount = bail.Amount + " " + bail.Account.CurrencyShortcut;
                            fam.GuarantorName = bail.Guarantor.Name.FullName();
                        }
                    }
                }
            })).Start();
        }
        public async void Update(int familyId)
        {
            var sourceFamily = await _apiClient.FamiliesController_GetAsync(familyId);
            var orphansCountTask = _apiClient.FamiliesController_GetOrphansCountAsync(familyId);
            //update father object in the source list
            var sourceFamilyIndex = _SourceFamilies.IndexOf(_SourceFamilies.FirstOrDefault(f => f.Id == familyId));
            _SourceFamilies[sourceFamilyIndex] = sourceFamily;

            var familyModel = _mapperService.MapToFamilyModel(sourceFamily);
            var FamilyToEdit = Families.FirstOrDefault(c => c.Id == familyId);
            var FamilyToEditIndex = Families.IndexOf(FamilyToEdit);
            familyModel.OrphansCount = await orphansCountTask;
            Families[FamilyToEditIndex] = familyModel;
        }

        public async Task<long?> SetColor(int familyId, long? colorValue)
        {
            long? returnedColor = null;

            var family = _SourceFamilies.FirstOrDefault(c => c.Id == familyId);
            returnedColor = family.ColorMark;
            if (colorValue != Color.White.ToArgb() && colorValue != Color.Black.ToArgb())
                family.ColorMark = colorValue;
            else
                family.ColorMark = null;
            await _apiClient.FamiliesController_PutAsync(family);
            return family.ColorMark;

        }

        public IList<int> OrphansIds(int familyId)
        {
            var family = _SourceFamilies.FirstOrDefault(c => c.Id == familyId);
            var orphansList = new List<int>();
            foreach (var orp in family.Orphans)
            {
                orphansList.Add(orp.Id);
            }
            if (orphansList != null && orphansList.Count > 0)
                return orphansList;
            else
                return null;
        }

        public IList<int> OrphansIds(IEnumerable<int> familiesIds)
        {
            IList<int> orphansList = new List<int>();
            foreach (var famId in familiesIds)
            {
                var retOrphanIds = OrphansIds(famId);
                if (retOrphanIds != null)
                    foreach (var orphId in retOrphanIds)
                    {
                        orphansList.Add(orphId);
                    }
            }
            if (orphansList != null && orphansList.Count > 0)
                return orphansList;
            else
                return null;
        }

        public int MothersIds(int familyId)
        {
            var family = _SourceFamilies.FirstOrDefault(c => c.Id == familyId);
            if (family != null)
                return family.MotherId;
            else
                return -1;
        }

        public IList<int> MothersIds(IEnumerable<int> familiesIds)
        {
            IList<int> motherList = new List<int>();
            foreach (var famId in familiesIds)
            {
                motherList.Add(MothersIds(famId));
            }
            if (motherList != null && motherList.Count > 0)
                return motherList;
            else
                return null;
        }

        public int FathersIds(int familyId)
        {
            var family = _SourceFamilies.FirstOrDefault(c => c.Id == familyId);
            if (family != null)
                return family.FatherId;
            else
                return -1;
        }

        public IList<int> FathersIds(IEnumerable<int> familiesIds)
        {
            IList<int> fatherList = new List<int>();
            foreach (var famId in familiesIds)
            {
                fatherList.Add(FathersIds(famId));
            }
            if (fatherList != null && fatherList.Count > 0)
                return fatherList;
            else
                return null;
        }

        public async Task Exclude(IEnumerable<int> familiesIds)
        {
            if (familiesIds == null) return;
            foreach (var famId in familiesIds)
            {
                var sourceFamily = _SourceFamilies.FirstOrDefault(o => o.Id == famId);
                var family = Families.FirstOrDefault(o => o.Id == famId);
                sourceFamily.IsExcluded = family.IsExcluded = true;
                await _apiClient.FamiliesController_PutAsync(sourceFamily);
            }
        }

        public async Task Delete(IEnumerable<int> familiesIds)
        {
            if (familiesIds == null) return;
            foreach (var famId in familiesIds)
            {
                var sourceFamily = _SourceFamilies.FirstOrDefault(o => o.Id == famId);
                var family = Families.FirstOrDefault(o => o.Id == famId);
                await _apiClient.FamiliesController_DeleteAsync(famId);
                _SourceFamilies.Remove(sourceFamily);
                Families.Remove(family);
            }

        }
    }
}
