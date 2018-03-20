using OrphanageDataModel.Persons;
using OrphanageV3.ViewModel.Caregiver;
using OrphanageV3.ViewModel.Mother;
using OrphanageV3.ViewModel.Orphan;
using OrphanageV3.ViewModel.Father;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Services.Interfaces
{
    public interface IMapperService
    {
        IEnumerable<OrphanModel> MapToOrphanModel(IEnumerable<Orphan> orphanList);
        OrphanModel MapToOrphanModel(Orphan orphan);

        IEnumerable<CaregiverModel> MapToCaregiverModel(IEnumerable<Caregiver> caregiverist);
        CaregiverModel MapToCaregiverModel(Caregiver caregiver);

        IEnumerable<MotherModel> MapToMotherModel(IEnumerable<Mother> mothersList);
        MotherModel MapToMotherModel(Mother mother);

        IEnumerable<FatherModel> MapToFatherModel(IEnumerable<Father> fathersList);
        FatherModel MapToFatherModel(Father father);
    }
}
