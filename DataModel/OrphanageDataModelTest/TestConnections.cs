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
            for (int i = 0; i < 10000; i++)
            {
                var xx = (from ss in dBContext.Orphans
                          where ss.Id == i
                          select ss).FirstOrDefault();

                var nn = (from ee in dBContext.Names
                          where ee.EnglishFirst != null
                          select ee).FirstOrDefault();

                var bails = dBContext.Bails.Where(tt=>tt.Id>10).FirstOrDefault();

                var sups = dBContext.Guarantors.FirstOrDefault();


                var caregiver = dBContext.Caregivers.FirstOrDefault();

                var father = dBContext.Fathers.FirstOrDefault();

                Assert.IsNotNull(xx.Name.First);
                
                GC.Collect();
            }
        }


    }
}
