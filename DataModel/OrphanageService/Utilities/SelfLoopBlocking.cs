using OrphanageDataModel.Persons;
using OrphanageService.Utilities.Interfaces;
using System;

namespace OrphanageService.Utilities
{
    public class SelfLoopBlocking : ISelfLoopBlocking
    {

        public void BlockCaregiverSelfLoop(ref OrphanageDataModel.Persons.Caregiver caregiver)
        {
            foreach (var orp in caregiver.Orphans)
            {
                orp.Caregiver = null;
            }
        }

        public void BlockFamilySelfLoop(ref OrphanageDataModel.RegularData.Family family)
        {
            if (family.Bail != null) family.Bail.Families = null;
            if (family.Father != null && family.Father.Families != null) family.Father.Families = null;
            if (family.Mother != null && family.Mother.Families != null) family.Mother.Families = null;
            if(family.Orphans != null)
            {
                foreach(var orp in family.Orphans)
                {
                    orp.Family = null;
                }
            }
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
