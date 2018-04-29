using NUnit.Framework;
using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using Shouldly;
using Unity;

namespace OrphanageServiceTests
{
    [TestFixture]
    public class TestCheckerService
    {
        private readonly ICheckerService _CheckerService = SetupFixture1.Container.Resolve<ICheckerService>();

        [Test]
        public void TestIsContactDataExist()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                OrphanageDataModel.RegularData.Address address = new Address()
                {
                    CellPhone = "(0933)068-734"
                };
                var ret = _CheckerService.IsContactDataExist(address, orphanageDbc).Result;
                ret.ShouldNotBeNull();
            }
        }

        [Test]
        public void TestIsFamilyCardNumberExist()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var ret = _CheckerService.IsFamilyCardNumberExist("1498906", orphanageDbc).Result;
                ret.ShouldNotBeNull();
            }
        }

        [Test]
        public void TestIsIdentityNumberExist()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var ret = _CheckerService.IsIdentityNumberExist("04180075865", orphanageDbc).Result;
                ret.ShouldNotBeNull();
            }
        }

        [Test]
        public void IsNameExist()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                OrphanageDataModel.RegularData.Name name = new Name()
                {
                    First = "إمل",
                    Father = "عمر",
                    Last = "النعسان"
                };

                var ret = _CheckerService.IsNameExist(name, orphanageDbc).Result;
                ret.ShouldNotBeNull();
            }
        }
    }
}