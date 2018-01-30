using OrphanageDataModel.FinancialData;
using OrphanageDataModel.Persons;
using OrphanageService.Utilities.Interfaces;

namespace OrphanageService.Utilities
{
    public class SelfLoopBlocking : ISelfLoopBlocking
    {
        public void BlockAccountSelfLoop(ref OrphanageDataModel.FinancialData.Account account)
        {
            if (account == null) return;

            if (account.Bails != null)
                foreach (var bail in account.Bails)
                    if (bail.Account != null) bail.Account = null;

            if (account.Guarantors != null)
                foreach (var guarantor in account.Guarantors)
                    if (guarantor.Account != null) guarantor.Account = null;

        }

        public void BlockBailSelfLoop(ref OrphanageDataModel.FinancialData.Bail bail)
        {
            if (bail == null) return;

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
            if (caregiver == null) return;

            foreach (var orp in caregiver.Orphans)
            {
                orp.Caregiver = null;
            }
        }

        public void BlockFamilySelfLoop(ref OrphanageDataModel.RegularData.Family family)
        {
            if (family == null) return;

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
            if (father == null) return;

            foreach (var fam in father.Families)
            {
                fam.Father = null;
            }
        }

        public void BlockGuarantorSelfLoop(ref OrphanageDataModel.Persons.Guarantor guarantor)
        {
            if (guarantor == null) return;

            if (guarantor.Account != null)
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
            if (mother == null) return;

            foreach (var fam in mother.Families)
            {
                fam.Mother = null;
            }
        }

        public void BlockOrphanSelfLoop(ref OrphanageDataModel.Persons.Orphan orphan)
        {
            if (orphan == null) return;

            orphan.Family.Orphans = null;
            if (orphan.Bail != null) orphan.Bail.Orphans=  null;
            if (orphan.Family.Father != null) orphan.Family.Father.Families = null;
            if (orphan.Family.Mother != null) orphan.Family.Mother.Families = null;
        }

        public void BlockUserSelfLoop(ref OrphanageDataModel.Persons.User user)
        {
            if (user == null) return;

            if (user.Accounts != null)
                foreach (var acc in user.Accounts)
                    acc.ActingUser = null;
            if (user.Bails != null)
                foreach (var bail in user.Bails)
                    bail.ActingUser = null;
            if (user.Caregivers != null)
                foreach (var careg in user.Caregivers)
                    careg.ActingUser = null;
            if (user.Famlies != null)
                foreach (var fam in user.Famlies)
                    fam.ActingUser = null;
            if (user.Fathers != null)
                foreach (var fath in user.Fathers)
                    fath.ActingUser = null;
            if (user.Guarantors != null)
                foreach (var gu in user.Fathers)
                    gu.ActingUser = null;
            if (user.Mothers != null)
                foreach (var mo in user.Mothers)
                    mo.ActingUser = null;
            if (user.Orphans != null)
                foreach (var orp in user.Orphans)
                    orp.ActingUser = null;
        }
    }
}
