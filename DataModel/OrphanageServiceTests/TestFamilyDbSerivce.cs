using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Unity;
using OrphanageService.Services.Interfaces;
using Shouldly;
namespace OrphanageServiceTests
{
    [TestFixture]
    public class TestFamilyDbSerivce
    {
        private readonly IFamilyDbService _familyDbService = SetupFixture1.Container.Resolve<IFamilyDbService>();

        [Test]
        public void TestAddFamily()
        {
            OrphanageDataModel.RegularData.Name nameF = TestDataStore.GetName(), nameM = TestDataStore.GetName();

            OrphanageDataModel.RegularData.Address addressM = TestDataStore.GetAddress(), addressFam = TestDataStore.GetAddress();
            OrphanageDataModel.RegularData.Family fam = new OrphanageDataModel.RegularData.Family()
            {
                Father = new OrphanageDataModel.Persons.Father()
                {
                    Birthday = new DateTime(1980, 1, 1),
                    DateOfDeath = DateTime.Now,
                    Name = nameF, RegDate = DateTime.Now, UserId = 1,
                    IdentityCardNumber = "0455468136665465"
                },
                Mother = new OrphanageDataModel.Persons.Mother()
                {
                    Name = nameM,
                    Address = addressM,
                    Birthday = new DateTime(1980, 1, 1),
                    HasSheOrphans = true,
                    IdentityCardNumber = "652987485464",
                    IsDead=false,
                    IsMarried=false,
                    RegDate=DateTime.Now,
                    UserId=1
                },
                PrimaryAddress =addressFam,
                UserId=1,
                RegDate=DateTime.Now,
                FinncialStatus = "TestFinnacialStatus",
                IsBailed =false,
                IsExcluded=false,
                IsTheyRefugees=false,
                ResidenceStatus = "TestResidenceStatus",
                ResidenceType = "TestResidenceType"                
            };
            var famId = _familyDbService.AddFamily(fam).Result;
            famId.ShouldBeGreaterThan(0);
            _familyDbService.DeleteFamily(fam.Id).Result.ShouldBe(true);
        }
    }
}
