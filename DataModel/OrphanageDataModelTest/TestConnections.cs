using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using OrphanageDBContext;

namespace OrphanageDataModelTest
{
    [TestClass]
    public class OrphanageDataModelTest
    {
        [TestMethod]
        public void TestCompatibility()
        {
            OrphansDBC dBContext = new OrphanageDBContext.OrphansDBC();
            var orphan = dBContext.Orphans.FirstOrDefault();

            var nn = (from ee in dBContext.Names
                      where ee.EnglishFirst != null
                      select ee).FirstOrDefault();

            var bails = dBContext.Bails.Where(tt => tt.Id > 10).FirstOrDefault();

            var sups = dBContext.Guarantors.FirstOrDefault();


            var caregiver = dBContext.Caregivers.FirstOrDefault();

            var father = dBContext.Fathers.FirstOrDefault();

            var guarantor = dBContext.Guarantors.FirstOrDefault();

            var mother = dBContext.Mothers.FirstOrDefault();

            var user = dBContext.Users.FirstOrDefault();

            var userCaregivers = user.Caregivers.Where(carG => carG.Id< 100);

            var Family = dBContext.Families.FirstOrDefault();

            var health = dBContext.Healthies.FirstOrDefault();

            var name = dBContext.Names.Where(n=> 
                        dBContext.Orphans.Count(o=>o.NameId == n.Id) == 0
            ).ToList();

            var study = dBContext.Studies.FirstOrDefault();

            var account = dBContext.Accounts.FirstOrDefault();

            Assert.IsNotNull(orphan.Name.First);
            Assert.IsNotNull(bails.Account);
            Assert.IsNotNull(sups.RegDate);
            Assert.IsNotNull(caregiver.Name.First);
            Assert.IsNotNull(father.Name.First);
            Assert.IsNotNull(mother.Name.First);
            Assert.IsNotNull(user.Name.First);
            Assert.IsNotNull(userCaregivers);
            Assert.IsNotNull(Family.PrimaryAddress);
            Assert.IsNotNull(account.AccountName);

        }


    }
}
