using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace OrphanageService.Services
{
    class AutoCompleteDbService : IAutoCompleteDbService
    {
        public async Task<IEnumerable<string>> GetCities()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Addresses.AsNoTracking()
                    .Select(x => x.City)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetCountries()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Addresses.AsNoTracking()
                    .Select(x => x.Country)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetEducationColleges()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Studies.AsNoTracking()
                    .Select(x => x.Collage)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetEducationReasons()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Studies.AsNoTracking()
                    .Select(x => x.Reasons)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetEducationSchools()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Studies.AsNoTracking()
                    .Select(x => x.School)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetEducationStages()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Studies.AsNoTracking()
                    .Select(x => x.Stage)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetEducationUniversities()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Studies.AsNoTracking()
                    .Select(x => x.Univercity)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetEnglishFatherNames()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Names.AsNoTracking()
                    .Select(x => x.EnglishFather)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetEnglishFirstNames()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Names.AsNoTracking()
                    .Select(x => x.EnglishFirst)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetEnglishLastNames()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Names.AsNoTracking()
                    .Select(x => x.EnglishLast)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetFatherNames()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Names.AsNoTracking()
                    .Select(x => x.Father)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetFathersDeathReasons()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Fathers.AsNoTracking()
                    .Select(x => x.DeathReason)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetFirstNames()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Names.AsNoTracking()
                    .Select(x => x.First)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetLastNames()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Names.AsNoTracking()
                    .Select(x => x.Last)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetMedicens()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Healthies.AsNoTracking()
                    .Select(x => x.Medicine)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetOrphansCaregiversConsanguinities()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Orphans.AsNoTracking()
                    .Select(x => x.ConsanguinityToCaregiver)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetOrphansPlacesOfBirth()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Orphans.AsNoTracking()
                    .Select(x => x.PlaceOfBirth)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetSicknessNames()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Healthies.AsNoTracking()
                    .Select(x => x.SicknessName)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetStreets()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Addresses.AsNoTracking()
                    .Select(x => x.Street)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }

        public async Task<IEnumerable<string>> GetTowns()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                var strings = await orphanageDbc.Addresses.AsNoTracking()
                    .Select(x => x.Town)
                    .Distinct()
                    .ToListAsync();
                return strings;
            }
        }
    }
}
