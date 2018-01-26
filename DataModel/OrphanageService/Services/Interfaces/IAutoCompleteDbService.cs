using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IAutoCompleteDbService
    {
        Task<IEnumerable<string>> GetFirstNames();

        Task<IEnumerable<string>> GetFatherNames();

        Task<IEnumerable<string>> GetLastNames();

        Task<IEnumerable<string>> GetEnglishFirstNames();

        Task<IEnumerable<string>> GetEnglishFatherNames();

        Task<IEnumerable<string>> GetEnglishLastNames();

        Task<IEnumerable<string>> GetCities();

        Task<IEnumerable<string>> GetTowns();

        Task<IEnumerable<string>> GetStreets();

        Task<IEnumerable<string>> GetCountries();

        Task<IEnumerable<string>> GetSicknessNames();

        Task<IEnumerable<string>> GetMedicens();

        Task<IEnumerable<string>> GetEducationStages();

        Task<IEnumerable<string>> GetEducationSchools();

        Task<IEnumerable<string>> GetEducationUniversities();

        Task<IEnumerable<string>> GetEducationColleges();

        Task<IEnumerable<string>> GetEducationReasons();

        Task<IEnumerable<string>> GetFathersDeathReasons();

        Task<IEnumerable<string>> GetOrphansCaregiversConsanguinities();

        Task<IEnumerable<string>> GetOrphansPlacesOfBirth();
    }
}
