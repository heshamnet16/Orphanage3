using OrphanageV3.ViewModel.Caregiver;
using OrphanageV3.ViewModel.Orphan;
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
    }
}
