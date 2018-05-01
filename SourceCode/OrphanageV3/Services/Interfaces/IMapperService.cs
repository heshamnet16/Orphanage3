using OrphanageDataModel.FinancialData;
using OrphanageDataModel.Persons;
using OrphanageDataModel.RegularData;
using OrphanageV3.ViewModel.Account;
using OrphanageV3.ViewModel.Bail;
using OrphanageV3.ViewModel.Caregiver;
using OrphanageV3.ViewModel.Family;
using OrphanageV3.ViewModel.Father;
using OrphanageV3.ViewModel.Guarantor;
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

        IEnumerable<FatherModel> MapToFatherModel(IEnumerable<Father> fathersList);

        FatherModel MapToFatherModel(Father father);

        IEnumerable<FamilyModel> MapToFamilyModel(IEnumerable<Family> familyList);

        FamilyModel MapToFamilyModel(Family family);

        IEnumerable<BailModel> MapToBailModel(IEnumerable<Bail> bailsList);

        BailModel MapToBailModel(Bail bail);

        IEnumerable<GuarantorModel> MapToGuarantorModel(IEnumerable<Guarantor> guarantorsList);

        GuarantorModel MapToGuarantorModel(Guarantor guarantor);

        IEnumerable<AccountModel> MapToAccountModel(IEnumerable<Account> accountsList);

        AccountModel MapToAccountModel(Account account);
    }
}