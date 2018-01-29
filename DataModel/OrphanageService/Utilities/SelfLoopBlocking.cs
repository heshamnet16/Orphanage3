using OrphanageDataModel.FinancialData;
using OrphanageService.Utilities.Interfaces;

namespace OrphanageService.Utilities
{
    public class SelfLoopBlocking : ISelfLoopBlocking
    {
        public void BlockAccountSelfLoop(ref OrphanageDataModel.FinancialData.Account account)
        {
            if (account.Bails != null)
                foreach (var bail in account.Bails)
                    if (bail.Account != null) bail.Account = null;

            if (account.Guarantors != null)
                foreach (var guarantor in account.Guarantors)
                    if (guarantor.Account != null) guarantor.Account = null;

        }

        public void BlockBailSelfLoop(ref OrphanageDataModel.FinancialData.Bail bail)
        {
            if (bail.Account != null) bail.Account.Bails = null;
            if (bail.Families != null)
            {
                foreach (var fam in bail.Families)
                {
                    fam.Bail = null;
                    fam.Orphans = null;
                }
            }
            if (bail.Orphans != null)
            {
                foreach (var orp in bail.Orphans)
                {
                    orp.Bail = null;
                }
            }
        }

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

        public void BlockGuarantorSelfLoop(ref OrphanageDataModel.Persons.Guarantor guarantor)
        {
           if(guarantor.Account != null)
            {
                if(guarantor.Account.Guarantors != null)
                {
                    guarantor.Account.Guarantors = null;
                }
                if(guarantor.Account.Bails != null)
                {
                    guarantor.Account.Bails = null;
                }
            }
            if (guarantor.Bails != null)
            {
                foreach (var bail in guarantor.Bails)
                    bail.Guarantor = null;
            }
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
