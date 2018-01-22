using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Utilities.Interfaces
{
    public interface ISelfLoopBlocking
    {
        void BlockFatherSelfLoop(ref OrphanageDataModel.Persons.Father father);
        void BlockMotherSelfLoop(ref OrphanageDataModel.Persons.Mother mother);
        void BlockOrphanSelfLoop(ref OrphanageDataModel.Persons.Orphan orphan);
        void BlockFamilySelfLoop(ref OrphanageDataModel.RegularData.Family family);
        void BlockGuarantorSelfLoop(ref OrphanageDataModel.Persons.Guarantor guarantor);
        void BlockCaregiverSelfLoop(ref OrphanageDataModel.Persons.Caregiver caregiver);
    }
}
