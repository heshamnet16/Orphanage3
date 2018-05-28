using OrphanageDataModel.RegularData.DTOs;
using OrphanageService.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Office.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/office/excel")]
    public class ExcelController : ApiController
    {
        private readonly IExcelService _excelService;

        public ExcelController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpPost]
        [Route("")]
        public async Task<byte[]> CreateXlsx(ExportData exportData)
        {
            return await _excelService.ConvertToXlsx(exportData.Data);
        }
    }
}