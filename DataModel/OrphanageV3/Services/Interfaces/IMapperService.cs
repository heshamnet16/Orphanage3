using OrphanageDataModel.FinancialData;
using OrphanageDataModel.Persons;
using OrphanageDataModel.RegularData;
using OrphanageV3.ViewModel.Bail;
using OrphanageV3.ViewModel.Caregiver;
using OrphanageV3.ViewModel.Family;
using OrphanageV3.ViewModel.Father;
using OrphanageV3.ViewModel.Mother;
using OrphanageV3.ViewModel.Orphan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageV3.Services.Interfaces
{
    public interface IMapperService
    {
        IEnumerable<OrphanModel> MapToOrphanModel(IEnumerable<Orphan> orphanList);

        OrphanModel MapToOrphanModel(Orphan orphan);

        IEnumerable<CaregiverModel> MapToCaregiverModel(IEnumerable<Caregiver> caregiverist);

        CaregiverModel MapToCaregiverModel(Caregiver caregiver);

        Task<IEnumerable<MotherModel>> MapToMotherModel(IEnumerable<Mother> mothersList);

        Task<MotherModel> MapToMotherModel(Mother mother);

        Task<IEnumerable<FatherModel>> MapToFatherModel(IEnumerable<Father> fathersList);

        Task<FatherModel> MapToFatherModel(Father father);

        IEnumerable<FamilyModel> MapToFamilyModel(IEnumerable<Family> familyList);

        FamilyModel MapToFamilyModel(Family family);

        IEnumerable<BailModel> MapToBailModel(IEnumerable<Bail> bailsList);

        BailModel MapToBailModel(Bail bail);
    }
}