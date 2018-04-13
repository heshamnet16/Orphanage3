using OrphanageDataModel.RegularData;
using OrphanageV3.Services;
using OrphanageV3.Services.Interfaces;
using OrphanageV3.ViewModel.Caregiver;
using OrphanageV3.ViewModel.Family;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace OrphanageV3.ViewModel.Orphan
{
    public class AddOrphanViewModel
    {
        private readonly IApiClient _apiClient;
        private CaregiverEditViewModel _caregiverEditViewModel;
        private readonly IMapperService _mapper;
        private readonly OrphanViewModel _orphanViewModel;
        private readonly IExceptionHandler _exceptionHandler;
        private IEnumerable<OrphanageDataModel.RegularData.Family> _sourceFamilies;
        private IEnumerable<OrphanageDataModel.Persons.Caregiver> _sourceCaregivers;

        public IEnumerable<CaregiverModel> CaregiversSelectionList;
        public IEnumerable<FamilyModel> FamiliesSelectionList;

        public event EventHandler CaregiversSelectionListLoad;

        public event EventHandler FamiliesSelectionListLoad;

        public AddOrphanViewModel(IApiClient apiClient, CaregiverEditViewModel caregiverEditViewModel, IMapperService mapper,
            OrphanViewModel orphanViewModel, IExceptionHandler exceptionHandler)
        {
            _apiClient = apiClient;
            _caregiverEditViewModel = caregiverEditViewModel;
            _mapper = mapper;
            _orphanViewModel = orphanViewModel;
            _exceptionHandler = exceptionHandler;
        }

        /// <summary>
        /// returns the caregiver object that matches the mother id or null when the orphans have more than one mother
        /// </summary>
        /// <param name="familyId"></param>
        /// <returns></returns>
        public async Task<OrphanageDataModel.Persons.Caregiver> GetCaregiverFromMother(int familyId)
        {
            var family = await _apiClient.FamiliesController_GetAsync(familyId);
            var allOrphans = await getOrphanAndBrothers(familyId);
            var mother = await _apiClient.MothersController_GetAsync(family.MotherId);
            OrphanageDataModel.Persons.Caregiver caregiverToReturn = null;
            if (allOrphans != null && allOrphans.Count > 0)
            {// returns null when there is more than one mother
                foreach (var orp in allOrphans)
                {
                    if (orp.Family.MotherId != family.MotherId)
                        return null;
                }
                //check if there is a mother as caregiver in the family
                foreach (var orp in allOrphans)
                {
                    var caregiver = await _apiClient.CaregiversController_GetAsync(orp.CaregiverId);
                    if (caregiver.Name.Equals(mother.Name))
                    {
                        caregiverToReturn = caregiver;
                    }
                }
            }
            if (caregiverToReturn == null)
            {
                caregiverToReturn = new OrphanageDataModel.Persons.Caregiver()
                {
                    ActingUser = mother.ActingUser,
                    Address = mother.Address,
                    AddressId = -1,
                    ColorMark = mother.ColorMark,
                    Id = -1,
                    IdentityCardId = mother.IdentityCardNumber,
                    Income = mother.Salary,
                    Jop = mother.Jop,
                    Name = mother.Name,
                    NameId = -1,
                    Note = mother.Note,
                    UserId = mother.UserId,
                    IdentityCardImageBackURI = mother.IdentityCardBackURI,
                    IdentityCardImageFaceURI = mother.IdentityCardFaceURI
                };
                caregiverToReturn.Name.Id = -1;
            }
            if (caregiverToReturn.Address == null)
                caregiverToReturn.Address = new Address();
            else
                caregiverToReturn.Address.Id = -1;
            return caregiverToReturn;
        }

        /// <summary>
        /// returns the caregiver object that take care of the other brothers or null when the other brothers have more than one caregiver
        /// </summary>
        /// <param name="familyId"></param>
        /// <returns></returns>
        public async Task<OrphanageDataModel.Persons.Caregiver> GetCaregiverFromOrphans(int familyId)
        {
            var family = await _apiClient.FamiliesController_GetAsync(familyId);
            var allOrphans = await getOrphanAndBrothers(familyId);
            if (allOrphans == null) return null;
            var caregiverToReturnTask = _apiClient.CaregiversController_GetAsync(allOrphans[0].CaregiverId);
            int idToCompare = -1;
            // returns null when there is more than one caregiver
            foreach (var orp in allOrphans)
            {
                if (idToCompare == -1) idToCompare = orp.CaregiverId;
                if (orp.CaregiverId != idToCompare)
                    return null;
            }
            return await caregiverToReturnTask;
        }

        public async Task<IList<OrphanageDataModel.Persons.Orphan>> getOrphanAndBrothers(int familyId)
        {
            var orphans = await _apiClient.FamiliesController_GetFamilyOrphansAsync(familyId);
            IList<OrphanageDataModel.Persons.Orphan> allOrphans = new List<OrphanageDataModel.Persons.Orphan>();
            if (orphans != null)
            {
                foreach (var orp in orphans)
                {
                    var brothers = await _apiClient.OrphansController_GetBrothersAsync(orp.Id);
                    if (brothers != null)
                    {
                        foreach (var bro in brothers)
                        {
                            if (!allOrphans.Any(o => o.Id == bro.Id))
                            {
                                allOrphans.Add(bro);
                            }
                        }
                    }

                    if (!allOrphans.Any(o => o.Id == orp.Id))
                    {
                        allOrphans.Add(orp);
                    }
                }
                return allOrphans.Count > 0 ? allOrphans : null;
            }
            else
                return null;
        }

        public async Task<OrphanageDataModel.Persons.Caregiver> AddCaregiver(OrphanageDataModel.Persons.Caregiver caregiver)
        {
            if (caregiver == null) return null;
            if (caregiver.Id <= 0)
            {
                return await _caregiverEditViewModel.Add(caregiver);
            }
            else
            {
                return caregiver;
            }
        }

        public async void getCaregiverSelectionList()
        {
            var caregiversCount = await _apiClient.CaregiversController_GetCaregiversCountAsync();
            _sourceCaregivers = await _apiClient.CaregiversController_GetAllAsync(caregiversCount, 0);
            CaregiversSelectionList = _mapper.MapToCaregiverModel(_sourceCaregivers);
            CaregiversSelectionListLoad?.Invoke(null, null);
        }

        public async void getFamiliesSelectionList()
        {
            var familiesCount = await _apiClient.FamiliesController_GetFamiliesCountAsync();
            _sourceFamilies = await _apiClient.FamiliesController_GetAllAsync(familiesCount, 0);
            FamiliesSelectionList = _mapper.MapToFamilyModel(_sourceFamilies);
            UpdateFamilyOrphansCount();
            FamiliesSelectionListLoad?.Invoke(null, null);
        }

        private void UpdateFamilyOrphansCount()
        {
            new Thread(new ThreadStart(async () =>
            {
                foreach (var fam in FamiliesSelectionList)
                {
                    var value = await _apiClient.FamiliesController_GetOrphansCountAsync(fam.Id);
                    fam.OrphansCount = value;
                }
            })).Start();
        }

        public void LoadSelectionData()
        {
            getFamiliesSelectionList();
            getCaregiverSelectionList();
        }

        public OrphanageDataModel.RegularData.Family GetSourceFamily(int FamModelId)
        {
            return _sourceFamilies.FirstOrDefault(fam => fam.Id == FamModelId);
        }

        public OrphanageDataModel.Persons.Caregiver GetSourceCaregiver(int caregiverModelId) =>
                _sourceCaregivers.FirstOrDefault(c => c.Id == caregiverModelId);

        public async Task<Image> GetImage(string url)
        {
            if (url == null || url.Length == 0) return null;
            return await _apiClient.GetImage(url, new Size(256, 256), 75);
        }

        public async Task<bool> SendImage(string url, Image img)
        {
            if (url == null || url.Length == 0) return false;
            return await _apiClient.SetImage(url, img);
        }

        public Task<OrphanageDataModel.Persons.Orphan> Add(OrphanageDataModel.Persons.Orphan orphan) => _orphanViewModel.Add(orphan);
    }
}