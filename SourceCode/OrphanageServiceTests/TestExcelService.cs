using NUnit.Framework;
using OrphanageService.Services.Interfaces;
using Shouldly;
using System.Collections.Generic;
using System.IO;
using Unity;

namespace OrphanageServiceTests
{
    [TestFixture]
    public class TestExcelService
    {
        private readonly IExcelService _excelService = SetupFixture1.Container.Resolve<IExcelService>();

        private IDictionary<string, IList<string>> getTestData()
        {
            var ret = new Dictionary<string, IList<string>>();
            for (int col = 0; col <= 10; col++)
            {
                var listValues = new List<string>();
                for (int row = 0; row <= 500; row++)
                {
                    listValues.Add(row.ToString());
                }
                ret.Add(col.ToString(), listValues);
            }
            return ret;
        }

        [Test]
        public void TestConvertToXlsx()
        {
            var ret = _excelService.ConvertToXlsx(getTestData()).Result;
            File.WriteAllBytes("d:\\ttt.xlsx", ret);
            ret.ShouldNotBeNull();
        }
    }
}