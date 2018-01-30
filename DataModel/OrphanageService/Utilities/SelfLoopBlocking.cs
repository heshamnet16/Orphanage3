using OrphanageDataModel.FinancialData;
using OrphanageDataModel.Persons;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;

namespace OrphanageService.Utilities
{
    public class SelfLoopBlocking : ISelfLoopBlocking
    {
        private static void BlockForignKeys(ref dynamic obj)
        {
            if (obj == null) return;
            if (obj is OrphanageDataModel.RegularData.Address address)
            {
                if (address.Families != null) address.Families = null;
                if (address.Caregivers != null) address.Caregivers = null;
                if (address.FamliesAlternativeAddresses != null) address.FamliesAlternativeAddresses = null;
                if (address.Guarantors != null) address.Guarantors = null;
                if (address.Mothers != null) address.Mothers = null;
                if (address.Users != null) address.Users = null;
            }
            else if (obj is OrphanageDataModel.RegularData.Name name)
            {
                if (name.Caregivers != null) name.Caregivers = null;
                if (name.Guarantors != null) name.Guarantors = null;
                if (name.Mothers != null) name.Mothers = null;
                if (name.Users != null) name.Users = null;
                if (name.Fathers != null) name.Fathers = null;
                if (name.Orphans != null) name.Orphans = null;
            }
            else if (obj is OrphanageDataModel.RegularData.Study study)
            {
                if (study.Orphans != null) study.Orphans = null;
            }
            else if (obj is OrphanageDataModel.RegularData.Health health)
            {
                if (health.Orphans != null) health.Orphans = null;
            }
        }

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
            if (bail.Guarantor != null)
            {
                var gua = bail.Guarantor;
                if(gua.Bails != null) gua.Bails = null;
                BlockGuarantorSelfLoop(ref gua);
                bail.Guarantor = gua;                
            }
        }

        public void BlockCaregiverSelfLoop(ref OrphanageDataModel.Persons.Caregiver caregiver)
        {
            if (caregiver == null) return;

            dynamic caregiverN = caregiver.Name;
            BlockForignKeys(ref caregiverN);
            caregiver.Name = caregiverN;

            dynamic caregiverA = caregiver.Address;
            BlockForignKeys(ref caregiverA);
            caregiver.Address = caregiverA;

            if (caregiver.Orphans != null)
            {
                foreach (var orp in caregiver.Orphans)
                {
                    orp.Caregiver = null;
                }
            }
        }

        public void BlockFamilySelfLoop(ref OrphanageDataModel.RegularData.Family family)
        {
            if (family == null) return;

            dynamic famA = family.AlternativeAddress;
            BlockForignKeys(ref famA);
            family.AlternativeAddress = famA;

            dynamic famA2 = family.PrimaryAddress;
            BlockForignKeys(ref famA2);
            family.PrimaryAddress = famA2;

            if (family.Bail != null) family.Bail.Families = null;
            if (family.Father != null)
            {
                var fath = family.Father;
                BlockFatherSelfLoop(ref fath);
                family.Father = fath;
                if (family.Father.Families != null) family.Father.Families = null;
            }
            if (family.Mother != null)
            {
                var moth = family.Mother;
                BlockMotherSelfLoop(ref moth);
                family.Mother = moth;
                if (family.Mother.Families != null) family.Mother.Families = null;
            }
            if (family.Orphans != null)
            {
                foreach (var orp in family.Orphans)
                {
                    orp.Family = null;
                }
            }
        }

        public void BlockFatherSelfLoop(ref OrphanageDataModel.Persons.Father father)
        {
            if (father == null) return;

            dynamic fathN = father.Name;
            BlockForignKeys(ref fathN);
            father.Name = fathN;
            if (father.Families != null)
            {
                var familesList = new List<OrphanageDataModel.RegularData.Family>();
                foreach (var fam in father.Families)
                {
                    fam.Father = null;
                    var famToblock = fam;
                    BlockFamilySelfLoop(ref famToblock);
                    familesList.Add(famToblock);
                }
                father.Families = familesList;
            }
        }

        public void BlockGuarantorSelfLoop(ref OrphanageDataModel.Persons.Guarantor guarantor)
        {
            if (guarantor == null) return;

            dynamic guarantorN = guarantor.Name;
            BlockForignKeys(ref guarantorN);
            guarantor.Name = guarantorN;

            dynamic guarantorA = guarantor.Address;
            BlockForignKeys(ref guarantorA);
            guarantor.Address = guarantorA;


            if (guarantor.Account != null)
            {
                if (guarantor.Account.Guarantors != null)
                {
                    guarantor.Account.Guarantors = null;
                }
                if (guarantor.Account.Bails != null)
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

            dynamic mothN = mother.Name;
            BlockForignKeys(ref mothN);
            mother.Name = mothN;

            dynamic mothA = mother.Address;
            BlockForignKeys(ref mothA);
            mother.Address = mothA;

            if (mother.Families != null)
            {
                var familesList = new List<OrphanageDataModel.RegularData.Family>();
                foreach (var fam in mother.Families)
                {
                    fam.Mother = null;
                    var famToblock = fam;
                    BlockFamilySelfLoop(ref famToblock);
                    familesList.Add(famToblock);
                }
                mother.Families = familesList;
            }
        }

        public void BlockOrphanSelfLoop(ref OrphanageDataModel.Persons.Orphan orphan)
        {
            if (orphan == null) return;

            dynamic orpN = orphan.Name;
            BlockForignKeys(ref orpN);
            orphan.Name = orpN;

            dynamic orpH = orphan.HealthStatus;
            BlockForignKeys(ref orpH);
            orphan.HealthStatus = orpH;

            dynamic orpE = orphan.Education;
            BlockForignKeys(ref orpE);
            orphan.Education = orpE;

            if (orphan.Family != null)
            {
                orphan.Family.Orphans = null;
                if (orphan.Family.Father != null) orphan.Family.Father.Families = null;
                if (orphan.Family.Mother != null) orphan.Family.Mother.Families = null;
                var fam = orphan.Family;
                BlockFamilySelfLoop(ref fam);
                orphan.Family = fam;
            }
            if (orphan.Bail != null) orphan.Bail.Orphans = null;
            if (orphan.Caregiver != null)
            {
                if (orphan.Caregiver.Orphans != null) orphan.Caregiver.Orphans = null;
                var caregiver = orphan.Caregiver;
                BlockCaregiverSelfLoop(ref caregiver);
                orphan.Caregiver = caregiver;
            }
        }

        public void BlockUserSelfLoop(ref OrphanageDataModel.Persons.User user)
        {
            if (user == null) return;

            dynamic userN = user.Name;
            BlockForignKeys(ref userN);
            user.Name = userN;

            dynamic userA = user.Address;
            BlockForignKeys(ref userA);
            user.Address = userA;


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
