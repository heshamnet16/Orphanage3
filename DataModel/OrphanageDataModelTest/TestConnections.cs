using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OrphanageDataModelTest
{
    [TestClass]
    public class OrphanageDataModelTest
    {
        [TestMethod]
        public void TestCompatibility()
        {
            OrphanageDBContext.OrphanageDBContext dBContext = new OrphanageDBContext.OrphanageDBContext();
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

            var Family = dBContext.Families.FirstOrDefault();

            var health = dBContext.Healthes.FirstOrDefault();

            var name = dBContext.Names.FirstOrDefault();

            var study = dBContext.Studies.FirstOrDefault();

            Assert.IsNotNull(orphan.Name.First);

        }


    }
}
