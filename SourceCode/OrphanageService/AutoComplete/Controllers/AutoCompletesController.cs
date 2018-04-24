using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.AutoComplete.Controllers
{
    [Authorize]
    [RoutePrefix("api/autocomplete")]
    public class AutoCompletesController : ApiController
    {
        private readonly IAutoCompleteDbService _AutoCompleteDbService;

        public AutoCompletesController(IAutoCompleteDbService autoCompleteDbService)
        {
            _AutoCompleteDbService = autoCompleteDbService;
        }

        //api/autocomplete/cities
        [HttpGet]
        [Route("cities")]
        public async Task<IEnumerable<string>> GetCities()
        {
            return await _AutoCompleteDbService.GetCities();
        }

        [HttpGet]
        [Route("Countries")]
        public async Task<IEnumerable<string>> GetCountries()
        {
            return await _AutoCompleteDbService.GetCountries();
        }

        [HttpGet]
        [Route("EducationColleges")]
        public async Task<IEnumerable<string>> GetEducationColleges()
        {
            return await _AutoCompleteDbService.GetEducationColleges();
        }

        [HttpGet]
        [Route("EducationReasons")]
        public async Task<IEnumerable<string>> GetEducationReasons()
        {
            return await _AutoCompleteDbService.GetEducationReasons();
        }

        [HttpGet]
        [Route("EducationSchools")]
        public async Task<IEnumerable<string>> GetEducationSchools()
        {
            return await _AutoCompleteDbService.GetEducationSchools();
        }

        [HttpGet]
        [Route("EducationStages")]
        public async Task<IEnumerable<string>> GetEducationStages()
        {
            return await _AutoCompleteDbService.GetEducationStages();
        }

        [HttpGet]
        [Route("EducationUniversities")]
        public async Task<IEnumerable<string>> GetEducationUniversities()
        {
            return await _AutoCompleteDbService.GetEducationUniversities();
        }

        [HttpGet]
        [Route("EnglishFatherNames")]
        public async Task<IEnumerable<string>> GetEnglishFatherNames()
        {
            return await _AutoCompleteDbService.GetEnglishFatherNames();
        }

        [HttpGet]
        [Route("EnglishFirstNames")]
        public async Task<IEnumerable<string>> GetEnglishFirstNames()
        {
            return await _AutoCompleteDbService.GetEnglishFirstNames();
        }

        [HttpGet]
        [Route("EnglishLastNames")]
        public async Task<IEnumerable<string>> GetEnglishLastNames()
        {
            return await _AutoCompleteDbService.GetEnglishLastNames();
        }

        [HttpGet]
        [Route("FatherNames")]
        public async Task<IEnumerable<string>> GetFatherNames()
        {
            return await _AutoCompleteDbService.GetFatherNames();
        }

        [HttpGet]
        [Route("FathersDeathReasons")]
        public async Task<IEnumerable<string>> GetFathersDeathReasons()
        {
            return await _AutoCompleteDbService.GetFathersDeathReasons();
        }

        [HttpGet]
        [Route("FirstNames")]
        public async Task<IEnumerable<string>> GetFirstNames()
        {
            return await _AutoCompleteDbService.GetFirstNames();
        }

        [HttpGet]
        [Route("LastNames")]
        public async Task<IEnumerable<string>> GetLastNames()
        {
            return await _AutoCompleteDbService.GetLastNames();
        }

        [HttpGet]
        [Route("Medicens")]
        public async Task<IEnumerable<string>> GetMedicens()
        {
            return await _AutoCompleteDbService.GetMedicens();
        }

        [HttpGet]
        [Route("OrphansCaregiversConsanguinities")]
        public async Task<IEnumerable<string>> GetOrphansCaregiversConsanguinities()
        {
            return await _AutoCompleteDbService.GetOrphansCaregiversConsanguinities();
        }

        [HttpGet]
        [Route("OrphansPlacesOfBirth")]
        public async Task<IEnumerable<string>> GetOrphansPlacesOfBirth()
        {
            return await _AutoCompleteDbService.GetOrphansPlacesOfBirth();
        }

        [HttpGet]
        [Route("SicknessNames")]
        public async Task<IEnumerable<string>> GetSicknessNames()
        {
            return await _AutoCompleteDbService.GetSicknessNames();
        }

        [HttpGet]
        [Route("Streets")]
        public async Task<IEnumerable<string>> GetStreets()
        {
            return await _AutoCompleteDbService.GetStreets();
        }

        [HttpGet]
        [Route("Towns")]
        public async Task<IEnumerable<string>> GetTowns()
        {
            return await _AutoCompleteDbService.GetTowns();
        }
    }
}