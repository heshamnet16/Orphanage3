using NUnit.Framework;
using OrphanageService.Services.Interfaces;
using Shouldly;
using System;
using System.Data.Entity.Validation;
using Unity;

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
            nameM.Last = "asdasd";

            OrphanageDataModel.RegularData.Address addressM = TestDataStore.GetAddress(), addressFam = TestDataStore.GetAddress();
            OrphanageDataModel.RegularData.Family fam = new OrphanageDataModel.RegularData.Family()
            {
                Father = new OrphanageDataModel.Persons.Father()
                {
                    Birthday = new DateTime(1980, 1, 1),
                    DateOfDeath = DateTime.Now,
                    Name = nameF,
                    RegDate = DateTime.Now,
                    UserId = 1,
                    IdentityCardNumber = "04554681365"
                },
                Mother = new OrphanageDataModel.Persons.Mother()
                {
                    Name = nameM,
                    Address = addressM,
                    Birthday = new DateTime(1980, 1, 1),
                    HasSheOrphans = true,
                    IdentityCardNumber = "65298748546",
                    IsDead = false,
                    IsMarried = false,
                    RegDate = DateTime.Now,
                    UserId = 1
                },
                PrimaryAddress = addressFam,
                UserId = 1,
                RegDate = DateTime.Now,
                FinncialStatus = "TestFinnacialStatus",
                IsBailed = false,
                IsExcluded = false,
                IsTheyRefugees = false,
                ResidenceStatus = "TestResidenceStatus",
                ResidenceType = "TestResidenceType"
            };
            try
            {
                var famId = _familyDbService.AddFamily(fam).Result;
                famId.Id.ShouldBeGreaterThan(0);
                _familyDbService.DeleteFamily(fam.Id).Result.ShouldBe(true);
            }
            catch (DbEntityValidationException exc)
            {
            }
            catch (Exception ex)
            {
            }
        }

        [Test]
        public void TestSaveFamily()
        {
            var family = _familyDbService.GetFamily(555).Result;
            family.Father.Name.EnglishFather = "EFatherEnglish";
            family.Mother.Name.EnglishFather = "EFatherEnglish";
            if (family.Mother.Address != null)
            {
                family.Mother.Address.City = "city";
                family.Mother.Address.Street = "street";
            }
            if (family.PrimaryAddress != null)
            {
                family.PrimaryAddress.City = "city";
                family.PrimaryAddress.Street = "street";
            }
            if (family.AlternativeAddress != null)
            {
                family.AlternativeAddress.City = "city";
                family.AlternativeAddress.Street = "street";
            }
            family.FinncialStatus = family.FinncialStatus + "_Test";
            var ret = _familyDbService.SaveFamily(family).Result;
            ret.ShouldBe(true);
            var newFamily = _familyDbService.GetFamily(555).Result;
            newFamily.Father.Name.EnglishFather.ShouldBe("EFatherEnglish");
            newFamily.Mother.Name.EnglishFather.ShouldBe("EFatherEnglish");
            if (family.PrimaryAddress != null) family.PrimaryAddress.Street.ShouldBe("street");
            if (family.AlternativeAddress != null) family.AlternativeAddress.Street.ShouldBe("street");
            family.FinncialStatus.EndsWith("_Test").ShouldBe(true);
        }
    }
}