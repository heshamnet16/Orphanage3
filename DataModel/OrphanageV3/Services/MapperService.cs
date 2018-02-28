using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OrphanageV3.Services.Interfaces;
using OrphanageV3.ViewModel.Orphan;
using Unity;
namespace OrphanageV3.Services
{
    public class MapperService : IMapperService
    {
        private static readonly IMapper _mapper;

        static MapperService()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                cfg.CreateMap<Orphan, OrphanModel>()
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
                .ForMember(dest => dest.StudyReasons, sour => sour.MapFrom(prop => prop.Education.Reasons))
                .ForMember(dest => dest.StudyStage, sour => sour.MapFrom(prop => prop.Education.Stage));
            });

            _mapper = mapperConfiguration.CreateMapper();
        }

        public MapperService() { }

        public IEnumerable<OrphanModel> MapToOrphanModel(IEnumerable<Orphan> orphanList)
        {
            foreach(var orp in orphanList)
            {

                OrphanModel retOrp = null;
                try
                {
                    retOrp = _mapper.Map<OrphanModel>(orp);
                    retOrp.IsSick = orp.HealthStatus != null ? true : false;
                }
                catch
                {
                    retOrp = null;
                }
                yield return retOrp;
            }
        }
    }
}
