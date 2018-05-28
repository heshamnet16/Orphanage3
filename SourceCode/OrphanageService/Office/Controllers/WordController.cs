using OrphanageDataModel.RegularData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Office.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/office/excel")]
    public class WordController : ApiController
    {
        private readonly IWordService _wordService;

        public WordController(IWordService wordService)
        {
            _wordService = wordService;
        }

        [HttpPost]
        [Route("")]
        public async Task<byte[]> CreateXlsx(IList<WordPage> exportData)
        {
            return await _wordService.ConvertToXlsx(exportData.Data);
        }
    }
}