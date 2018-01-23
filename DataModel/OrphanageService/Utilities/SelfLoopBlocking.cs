using OrphanageDataModel.Persons;
using OrphanageDataModel.RegularData;
using OrphanageService.Utilities.Interfaces;
using System;

namespace OrphanageService.Utilities
{
    public class SelfLoopBlocking : ISelfLoopBlocking
    {

        public void BlockCaregiverSelfLoop(ref Caregiver caregiver)
        {
            foreach (var orp in caregiver.Orphans)
            {
                orp.Caregiver = null;
            }
        }

        public void BlockFamilySelfLoop(ref Family family)
        {
            if (!family.Bail.Equals(null)) family.Bail.Families = null;
        }

        public void BlockFatherSelfLoop(ref OrphanageDataModel.Persons.Father father)
        {
            foreach(var fam in father.Families)
            {
                fam.Father = null;
            }
        }

        public void BlockGuarantorSelfLoop(ref Guarantor guarantor)
        {
            throw new NotImplementedException();
        }

        public void BlockMotherSelfLoop(ref OrphanageDataModel.Persons.Mother mother)
        {
            foreach (var fam in mother.Families)
            {
                fam.Mother = null;
            }
        }

        public void BlockOrphanSelfLoop(ref OrphanageDataModel.Persons.Orphan orphan)
        {
            orphan.Family.Orphans = null;
            if (orphan.Bail != null) orphan.Bail.Orphans=  null;
            if (orphan.Family.Father != null) orphan.Family.Father.Families = null;
            if (orphan.Family.Mother != null) orphan.Family.Mother.Families = null;
        }
    }
}
