﻿using AutoMapper;
using OrphanageDataModel.Persons;
using OrphanageV3.Services.Interfaces;
using OrphanageV3.ViewModel.Caregiver;
using OrphanageV3.ViewModel.Father;
using OrphanageV3.ViewModel.Mother;
using OrphanageV3.ViewModel.Orphan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageV3.Services
{
    public class MapperService : IMapperService
    {
        private static readonly IMapper _mapper;
        private readonly IDataFormatterService _dataFormatterService;
        private readonly IApiClient _ApiClient;

        static MapperService()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                cfg.CreateMap<OrphanageDataModel.Persons.Orphan, OrphanModel>()
                .ForMember(dest => dest.FirstName, sour => sour.MapFrom(prop => prop.Name.First))
                .ForMember(dest => dest.FatherName, sour => sour.MapFrom(prop => prop.Name.Father))
                .ForMember(dest => dest.LastName, sour => sour.MapFrom(prop => prop.Name.Last))
                .ForMember(dest => dest.GrandFatherName, sour => sour.MapFrom(prop => prop.Family.Father.Name.Father))
                .ForMember(dest => dest.MotherFatherName, sour => sour.MapFrom(prop => prop.Family.Mother.Name.Father))
                .ForMember(dest => dest.MotherFirstName, sour => sour.MapFrom(prop => prop.Family.Mother.Name.First))
                .ForMember(dest => dest.MotherLastName, sour => sour.MapFrom(prop => prop.Family.Mother.Name.Last))
                .ForMember(dest => dest.CaregiverFatherName, sour => sour.MapFrom(prop => prop.Caregiver.Name.Father))
                .ForMember(dest => dest.CaregiverFirstName, sour => sour.MapFrom(prop => prop.Caregiver.Name.First))
                .ForMember(dest => dest.CaregiverLastName, sour => sour.MapFrom(prop => prop.Caregiver.Name.Last))
                .ForMember(dest => dest.FatherID, sour => sour.MapFrom(prop => prop.Family.FatherId))
                .ForMember(dest => dest.MotherID, sour => sour.MapFrom(prop => prop.Family.MotherId))
                .ForMember(dest => dest.ColorMark, sour => sour.MapFrom(prop => prop.ColorMark))
                .ForMember(dest => dest.Birthplace, sour => sour.MapFrom(prop => prop.PlaceOfBirth))
                .ForMember(dest => dest.IdentityCardNumber, sour => sour.MapFrom(prop => prop.IdentityCardNumber))
                .ForMember(dest => dest.Story, sour => sour.MapFrom(prop => prop.Story))
                .ForMember(dest => dest.StudyReasons, sour => sour.MapFrom(prop => prop.Education.Reasons))
                .ForMember(dest => dest.StudyStage, sour => sour.MapFrom(prop => prop.Education.Stage));

                cfg.CreateMap<OrphanageDataModel.Persons.Caregiver, CaregiverModel>()
                .ForMember(dest => dest.FirstName, sour => sour.MapFrom(prop => prop.Name.First))
                .ForMember(dest => dest.FatherName, sour => sour.MapFrom(prop => prop.Name.Father))
                .ForMember(dest => dest.LastName, sour => sour.MapFrom(prop => prop.Name.Last))
                .ForMember(dest => dest.CellPhone, sour => sour.MapFrom(prop => prop.Address.CellPhone))
                .ForMember(dest => dest.WorkPhone, sour => sour.MapFrom(prop => prop.Address.WorkPhone))
                .ForMember(dest => dest.HomePhone, sour => sour.MapFrom(prop => prop.Address.HomePhone))
                .ForMember(dest => dest.IdentityCardNumber, sour => sour.MapFrom(prop => prop.IdentityCardId))
                .ForMember(dest => dest.ColorMark, sour => sour.MapFrom(prop => prop.ColorMark))
                .ForMember(dest => dest.Notes, sour => sour.MapFrom(prop => prop.Note))
                .ForMember(dest => dest.UserName, sour => sour.MapFrom(prop => prop.ActingUser.UserName));

                cfg.CreateMap<OrphanageDataModel.Persons.Mother, MotherModel>()
                    .ForMember(dest => dest.FirstName, sour => sour.MapFrom(prop => prop.Name.First))
                    .ForMember(dest => dest.FatherName, sour => sour.MapFrom(prop => prop.Name.Father))
                    .ForMember(dest => dest.LastName, sour => sour.MapFrom(prop => prop.Name.Last))
                    .ForMember(dest => dest.CellPhone, sour => sour.MapFrom(prop => prop.Address.CellPhone))
                    .ForMember(dest => dest.WorkPhone, sour => sour.MapFrom(prop => prop.Address.WorkPhone))
                    .ForMember(dest => dest.HomePhone, sour => sour.MapFrom(prop => prop.Address.HomePhone))
                    .ForMember(dest => dest.Notes, sour => sour.MapFrom(prop => prop.Note))
                    .ForMember(dest => dest.UserName, sour => sour.MapFrom(prop => prop.ActingUser.UserName));

                cfg.CreateMap<OrphanageDataModel.Persons.Father, FatherModel>()
                    .ForMember(dest => dest.FirstName, sour => sour.MapFrom(prop => prop.Name.First))
                    .ForMember(dest => dest.FatherName, sour => sour.MapFrom(prop => prop.Name.Father))
                    .ForMember(dest => dest.LastName, sour => sour.MapFrom(prop => prop.Name.Last))
                    .ForMember(dest => dest.Notes, sour => sour.MapFrom(prop => prop.Note))
                    .ForMember(dest => dest.UserName, sour => sour.MapFrom(prop => prop.ActingUser.UserName));
            });

            _mapper = mapperConfiguration.CreateMapper();
        }

        public MapperService(IDataFormatterService dataFormatterService, IApiClient apiClient)
        {
            _dataFormatterService = dataFormatterService;
            _ApiClient = apiClient;
        }

        public IEnumerable<OrphanModel> MapToOrphanModel(IEnumerable<OrphanageDataModel.Persons.Orphan> orphanList)
        {
            foreach (var orp in orphanList)
            {
                OrphanModel retOrp = null;
                try
                {
                    retOrp = MapToOrphanModel(orp);
                }
                catch
                {
                    retOrp = null;
                }
                yield return retOrp;
            }
        }

        public OrphanModel MapToOrphanModel(OrphanageDataModel.Persons.Orphan orphan)
        {
            OrphanModel retOrp = null;
            try
            {
                retOrp = _mapper.Map<OrphanModel>(orphan);
                retOrp.IsSick = orphan.HealthStatus != null ? true : false;
                retOrp.FullName = _dataFormatterService.GetFullNameString(orphan.Name);
                retOrp.CaregiverFullName = _dataFormatterService.GetFullNameString(orphan.Caregiver?.Name);
                retOrp.FatherFullName = _dataFormatterService.GetFullNameString(orphan.Family?.Father?.Name);
                retOrp.MotherFullName = _dataFormatterService.GetFullNameString(orphan.Family?.Mother?.Name);
                //TODO extend IsBailed to family bails
            }
            catch
            {
                retOrp = null;
            }
            return retOrp;
        }

        public IEnumerable<CaregiverModel> MapToCaregiverModel(IEnumerable<OrphanageDataModel.Persons.Caregiver> caregiverist)
        {
            foreach (var caregiver in caregiverist)
            {
                CaregiverModel retCaregiver = MapToCaregiverModel(caregiver);
                yield return retCaregiver;
            }
        }

        public CaregiverModel MapToCaregiverModel(OrphanageDataModel.Persons.Caregiver caregiver)
        {
            CaregiverModel retCaregiver = null;
            try
            {
                retCaregiver = _mapper.Map<CaregiverModel>(caregiver);
                retCaregiver.FullName = _dataFormatterService.GetFullNameString(caregiver.Name);
                retCaregiver.FullAddress = _dataFormatterService.GetAddressString(caregiver.Address);
                if (caregiver.Orphans != null && caregiver.Orphans.Count > 0)
                    retCaregiver.OrphansCount = caregiver.Orphans.Count;
                else
                    retCaregiver.OrphansCount = 0;
            }
            catch
            {
                retCaregiver = null;
            }
            return retCaregiver;
        }

        public async Task<IEnumerable<MotherModel>> MapToMotherModel(IEnumerable<Mother> mothersList)
        {
            IList<MotherModel> returnedList = new List<MotherModel>();
            foreach (var mother in mothersList)
            {
                MotherModel retMother = await MapToMotherModel(mother);
                returnedList.Add(retMother);
            }
            return returnedList;
        }

        public async Task<MotherModel> MapToMotherModel(Mother mother)
        {
            MotherModel retMother = null;
            try
            {
                retMother = _mapper.Map<MotherModel>(mother);
                retMother.FullName = _dataFormatterService.GetFullNameString(mother.Name);
                retMother.FullAddress = _dataFormatterService.GetAddressString(mother.Address);
                retMother.OrphansCount = -1;
                retMother.HusbandsNames = "";
                foreach (var fam in mother.Families)
                {
                    if (fam.Father == null || fam.Father.Name == null)
                    {
                        fam.Father = await _ApiClient.FathersController_GetAsync(fam.FatherId);
                    }
                    if (mother.Families.Count > 1)
                        retMother.HusbandsNames += _dataFormatterService.GetFullNameString(fam.Father.Name) + ", ";
                    else
                        retMother.HusbandsNames += _dataFormatterService.GetFullNameString(fam.Father.Name) + ", ";
                }
                if (retMother.HusbandsNames.EndsWith(", "))
                    retMother.HusbandsNames = retMother.HusbandsNames.Substring(0, retMother.HusbandsNames.Length - 2);
            }
            catch
            {
                retMother = null;
            }
            return retMother;
        }

        public async Task<IEnumerable<FatherModel>> MapToFatherModel(IEnumerable<Father> fathersList)
        {
            IList<FatherModel> returnedFathers = new List<FatherModel>();
            foreach (var father in fathersList)
            {
                FatherModel retFather = await MapToFatherModel(father);
                returnedFathers.Add(retFather);
            }
            return returnedFathers;
        }

        public async Task<FatherModel> MapToFatherModel(Father father)
        {
            FatherModel retFather = null;
            try
            {
                retFather = _mapper.Map<FatherModel>(father);
                retFather.FullName = _dataFormatterService.GetFullNameString(father.Name);
                retFather.WifeName = "";
                foreach (var fam in father.Families)
                {
                    if (fam.Mother == null || fam.Mother.Name == null)
                    {
                        fam.Mother = await _ApiClient.MothersController_GetAsync(fam.MotherId);
                    }
                    if (father.Families.Count > 1)
                        retFather.WifeName += _dataFormatterService.GetFullNameString(fam.Mother.Name) + ", ";
                    else
                        retFather.WifeName += _dataFormatterService.GetFullNameString(fam.Mother.Name) + ", ";
                }
                if (retFather.WifeName.EndsWith(", "))
                    retFather.WifeName = retFather.WifeName.Substring(0, retFather.WifeName.Length - 2);
                retFather.OrphansCount = -1;
            }
            catch
            {
                retFather = null;
            }
            return retFather;
        }
    }
}