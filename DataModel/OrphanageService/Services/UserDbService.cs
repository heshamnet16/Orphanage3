using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class UserDbService : IUserDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;

        public UserDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Account>> GetAccounts(int Uid)
        {
            IList<OrphanageDataModel.FinancialData.Account> accountsList = new List<OrphanageDataModel.FinancialData.Account>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var accounts = await _orphanageDBC.Accounts.AsNoTracking()
                    .Include(a => a.Bails)
                    .Include(b => b.Guarantors)
                    .Where(b=>b.UserId==Uid)
                    .ToListAsync();

                foreach (var account in accounts)
                {
                    OrphanageDataModel.FinancialData.Account accountToFill = account;
                    _selfLoopBlocking.BlockAccountSelfLoop(ref accountToFill);
                    accountsList.Add(accountToFill);
                }
            }
            return accountsList;
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Account>> GetAccounts(int Uid, int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.FinancialData.Account> accountsList = new List<OrphanageDataModel.FinancialData.Account>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int accountsCount = await _orphanageDBC.Bails.AsNoTracking().CountAsync();
                if (accountsCount < totalSkiped)
                {
                    totalSkiped = accountsCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var accounts = await _orphanageDBC.Accounts.AsNoTracking()
                    .OrderBy(c => c.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(a => a.Bails)
                    .Include(b => b.Guarantors)
                    .Where(b => b.UserId == Uid)
                    .ToListAsync();

                foreach (var account in accounts)
                {
                    OrphanageDataModel.FinancialData.Account accountToFill = account;
                    _selfLoopBlocking.BlockAccountSelfLoop(ref accountToFill);
                    accountsList.Add(accountToFill);
                }
            }
            return accountsList;
        }

        public async Task<int> GetAccountsCount(int Uid)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int accountsCount = await _orphanageDBC.Accounts.AsNoTracking()
                    .Where(a=>a.UserId==Uid)
                    .CountAsync();
                return accountsCount;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Uid)
        {
            IList<OrphanageDataModel.FinancialData.Bail> bailsList = new List<OrphanageDataModel.FinancialData.Bail>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var bails = await _orphanageDBC.Bails.AsNoTracking()
                    .Include(b => b.Account)
                    .Include(b => b.Guarantor)                   
                    .Where(b=>b.UserId==Uid)
                    .ToListAsync();

                foreach (var bail in bails)
                {
                    OrphanageDataModel.FinancialData.Bail bailsToFill = bail;
                    _selfLoopBlocking.BlockBailSelfLoop(ref bailsToFill);
                    bailsList.Add(bailsToFill);
                }
            }
            return bailsList;
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Uid, int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.FinancialData.Bail> bailsList = new List<OrphanageDataModel.FinancialData.Bail>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int bailsCount = await _orphanageDBC.Bails.AsNoTracking().CountAsync();
                if (bailsCount < totalSkiped)
                {
                    totalSkiped = bailsCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var bails = await _orphanageDBC.Bails.AsNoTracking()
                    .OrderBy(c => c.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(b => b.Account)
                    .Include(b => b.Guarantor)
                    .Where(b=>b.UserId==Uid)
                    .ToListAsync();

                foreach (var bail in bails)
                {
                    OrphanageDataModel.FinancialData.Bail bailsToFill = bail;
                    _selfLoopBlocking.BlockBailSelfLoop(ref bailsToFill);
                    bailsList.Add(bailsToFill);
                }
            }
            return bailsList;

        }

        public async Task<int> GetBailsCount(int Uid)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int bailsCount = await _orphanageDBC.Bails.AsNoTracking()
                    .Where(b=>b.UserId==Uid).CountAsync();
                return bailsCount;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Caregiver>> GetCaregivers(int Uid)
        {
            IList<OrphanageDataModel.Persons.Caregiver> caregiversList = new List<OrphanageDataModel.Persons.Caregiver>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var caregivers = await _orphanageDBC.Caregivers.AsNoTracking()
                    .Include(c => c.Address)
                    .Include(c => c.Name)
                    .Include(c => c.Orphans)
                    .Where(c=>c.UserId==Uid)
                    .ToListAsync();

                foreach (var caregiver in caregivers)
                {
                    OrphanageDataModel.Persons.Caregiver caregiverToFill = caregiver;
                    _selfLoopBlocking.BlockCaregiverSelfLoop(ref caregiverToFill);
                    _uriGenerator.SetCaregiverUris(ref caregiverToFill);
                    caregiversList.Add(caregiverToFill);
                }
            }
            return caregiversList;
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Caregiver>> GetCaregivers(int Uid, int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Caregiver> caregiversList = new List<OrphanageDataModel.Persons.Caregiver>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int caregiversCount = await _orphanageDBC.Caregivers.AsNoTracking().CountAsync();
                if (caregiversCount < totalSkiped)
                {
                    totalSkiped = caregiversCount - pageSize;
                }
                var caregivers = await _orphanageDBC.Caregivers.AsNoTracking()
                    .OrderBy(c => c.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(c => c.Address)
                    .Include(c => c.Name)
                    .Include(c => c.Orphans)
                    .Where(c => c.UserId == Uid)
                    .ToListAsync();

                foreach (var caregiver in caregivers)
                {
                    OrphanageDataModel.Persons.Caregiver caregiverToFill = caregiver;
                    _selfLoopBlocking.BlockCaregiverSelfLoop(ref caregiverToFill);
                    _uriGenerator.SetCaregiverUris(ref caregiverToFill);
                    caregiversList.Add(caregiverToFill);
                }
            }
            return caregiversList;
        }

        public async Task<int> GetCaregiversCount(int Uid)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int caregiversCount = await _orphanageDBC.Caregivers.AsNoTracking()
                    .Where(c => c.UserId == Uid)
                    .CountAsync();
                return caregiversCount;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int Uid)
        {
            IList<OrphanageDataModel.RegularData.Family> familiesList = new List<OrphanageDataModel.RegularData.Family>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var families = await _orphanageDBC.Families.AsNoTracking()
                    .Include(f => f.AlternativeAddress)
                    .Include(f => f.Bail)
                    .Include(f => f.Father)
                    .Include(f => f.Mother)
                    .Include(f => f.Orphans)
                    .Include(f => f.PrimaryAddress)
                    .Where(f=>f.UserId==Uid)
                    .ToListAsync();

                foreach (var family in families)
                {
                    OrphanageDataModel.RegularData.Family familyToFill = family;
                    _selfLoopBlocking.BlockFamilySelfLoop(ref familyToFill);
                    _uriGenerator.SetFamilyUris(ref familyToFill);
                    familiesList.Add(familyToFill);
                }
            }
            return familiesList;
        }

        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int Uid, int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.RegularData.Family> familiesList = new List<OrphanageDataModel.RegularData.Family>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int FamiliesCount = await _orphanageDBC.Fathers.AsNoTracking().CountAsync();
                if (FamiliesCount < totalSkiped)
                {
                    totalSkiped = FamiliesCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var families = await _orphanageDBC.Families.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(f => f.AlternativeAddress)
                    .Include(f => f.Bail)
                    .Include(f => f.Father)
                    .Include(f => f.Mother)
                    .Include(f => f.Orphans)
                    .Include(f => f.PrimaryAddress)
                    .Where(f => f.UserId == Uid)
                    .ToListAsync();

                foreach (var family in families)
                {
                    OrphanageDataModel.RegularData.Family familyToFill = family;
                    _selfLoopBlocking.BlockFamilySelfLoop(ref familyToFill);
                    _uriGenerator.SetFamilyUris(ref familyToFill);
                    familiesList.Add(familyToFill);
                }
            }
            return familiesList;
        }

        public async Task<int> GetFamiliesCount(int Uid)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int familiesCount = await _orphanageDBC.Families.AsNoTracking()
                    .Where(f=>f.UserId==Uid)
                    .CountAsync();
                return familiesCount;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(int Uid)
        {
            IList<OrphanageDataModel.Persons.Father> fathersList = new List<OrphanageDataModel.Persons.Father>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var fathers = await _orphanageDBC.Fathers.AsNoTracking()
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .Where(f=>f.UserId==Uid)
                    .ToListAsync();

                foreach (var father in fathers)
                {
                    OrphanageDataModel.Persons.Father fatherToFill = father;
                    FatherDbService.setFatherEntities(ref fatherToFill, _orphanageDBC);
                    _selfLoopBlocking.BlockFatherSelfLoop(ref fatherToFill);
                    _uriGenerator.SetFatherUris(ref fatherToFill);
                    fathersList.Add(fatherToFill);
                }
            }
            return fathersList;
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(int Uid, int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Father> fathersList = new List<OrphanageDataModel.Persons.Father>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int FathersCount = await _orphanageDBC.Fathers.AsNoTracking().CountAsync();
                if (FathersCount < totalSkiped)
                {
                    totalSkiped = FathersCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var fathers = await _orphanageDBC.Fathers.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .Where(f => f.UserId == Uid)
                    .ToListAsync();

                foreach (var father in fathers)
                {
                    OrphanageDataModel.Persons.Father fatherToFill = father;
                    FatherDbService.setFatherEntities(ref fatherToFill, _orphanageDBC);
                    _selfLoopBlocking.BlockFatherSelfLoop(ref fatherToFill);
                    _uriGenerator.SetFatherUris(ref fatherToFill);
                    fathersList.Add(fatherToFill);
                }
            }
            return fathersList;
        }

        public async Task<int> GetFathersCount(int Uid)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int orphansCount = await _orphanageDBC.Fathers.AsNoTracking()
                    .Where(f=>f.UserId==Uid)
                    .CountAsync();
                return orphansCount;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int Uid)
        {
            IList<OrphanageDataModel.Persons.Guarantor> guarantorsList = new List<OrphanageDataModel.Persons.Guarantor>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var guarantors = await _orphanageDBC.Guarantors.AsNoTracking()
                    .Include(g => g.Address)
                    .Include(c => c.Name)
                    .Include(g => g.Account)
                    .Where(g=>g.UserId==Uid)
                    .ToListAsync();

                foreach (var guarantor in guarantors)
                {
                    OrphanageDataModel.Persons.Guarantor guarantorToFill = guarantor;
                    _selfLoopBlocking.BlockGuarantorSelfLoop(ref guarantorToFill);
                    guarantorsList.Add(guarantorToFill);
                }
            }
            return guarantorsList;
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int Uid, int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Guarantor> guarantorsList = new List<OrphanageDataModel.Persons.Guarantor>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int guarantorsCount = await _orphanageDBC.Guarantors.AsNoTracking().CountAsync();
                if (guarantorsCount < totalSkiped)
                {
                    totalSkiped = guarantorsCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var guarantors = await _orphanageDBC.Guarantors.AsNoTracking()
                    .OrderBy(c => c.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(g => g.Address)
                    .Include(c => c.Name)
                    .Include(g => g.Account)
                    .Where(g => g.UserId == Uid)
                    .ToListAsync();

                foreach (var guarantor in guarantors)
                {
                    OrphanageDataModel.Persons.Guarantor guarantorToFill = guarantor;
                    _selfLoopBlocking.BlockGuarantorSelfLoop(ref guarantorToFill);
                    guarantorsList.Add(guarantorToFill);
                }
            }
            return guarantorsList;
        }

        public async Task<int> GetGuarantorsCount(int Uid)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int guarantorsCount = await _orphanageDBC.Guarantors.AsNoTracking()
                    .Where(g=>g.UserId==Uid)
                    .CountAsync();
                return guarantorsCount;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetMothers(int Uid)
        {
            IList<OrphanageDataModel.Persons.Mother> mothersList = new List<OrphanageDataModel.Persons.Mother>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var mothers = await _orphanageDBC.Mothers.AsNoTracking()
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .Where(m=>m.UserId==Uid)
                    .ToListAsync();

                foreach (var mother in mothers)
                {
                    OrphanageDataModel.Persons.Mother motherToFill = mother;
                    MotherDbService.setMotherEntities(ref motherToFill, _orphanageDBC);
                    _selfLoopBlocking.BlockMotherSelfLoop(ref motherToFill);
                    _uriGenerator.SetMotherUris(ref motherToFill);
                    mothersList.Add(motherToFill);
                }
            }
            return mothersList;
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetMothers(int Uid, int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Mother> mothersList = new List<OrphanageDataModel.Persons.Mother>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int MothersCount = await _orphanageDBC.Mothers.AsNoTracking().CountAsync();
                if (MothersCount < totalSkiped)
                {
                    totalSkiped = MothersCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var mothers = await _orphanageDBC.Mothers.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .Where(m => m.UserId == Uid)
                    .ToListAsync();

                foreach (var mother in mothers)
                {
                    OrphanageDataModel.Persons.Mother motherToFill = mother;
                    MotherDbService.setMotherEntities(ref motherToFill, _orphanageDBC);
                    _selfLoopBlocking.BlockMotherSelfLoop(ref motherToFill);
                    _uriGenerator.SetMotherUris(ref motherToFill);
                    mothersList.Add(motherToFill);
                }
            }
            return mothersList;
        }

        public async Task<int> GetMothersCount(int Uid)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int mothersCount = await _orphanageDBC.Mothers.AsNoTracking()
                    .Where(m=>m.UserId==Uid)
                    .CountAsync();
                return mothersCount;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Uid)
        {
            IList<OrphanageDataModel.Persons.Orphan> orphansList = new List<OrphanageDataModel.Persons.Orphan>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var orphans = await _orphanageDBC.Orphans.AsNoTracking()
                    .Include(o => o.Name)
                    .Include(o => o.Caregiver.Name)
                    .Include(o => o.Caregiver.Address)
                    .Include(o => o.Family.Father.Name)
                    .Include(o => o.Family.Mother.Name)
                    .Include(o => o.Family.PrimaryAddress)
                    .Include(o => o.Family.AlternativeAddress)
                    .Include(o => o.Guarantor.Name)
                    .Where(o=>o.UserId==Uid)
                    .ToListAsync();

                foreach (var orphan in orphans)
                {
                    var orphanTofill = orphan;
                    _selfLoopBlocking.BlockOrphanSelfLoop(ref orphanTofill);
                    _uriGenerator.SetOrphanUris(ref orphanTofill);
                    orphansList.Add(orphanTofill);
                }
            }
            return orphansList;
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Uid, int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Orphan> orphansList = new List<OrphanageDataModel.Persons.Orphan>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int orphansCount = await _orphanageDBC.Orphans.AsNoTracking().CountAsync();
                if (orphansCount < totalSkiped)
                {
                    totalSkiped = orphansCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var orphans = await _orphanageDBC.Orphans.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(o => o.Name)
                    .Include(o => o.Caregiver.Name)
                    .Include(o => o.Caregiver.Address)
                    .Include(o => o.Family.Father.Name)
                    .Include(o => o.Family.Mother.Name)
                    .Include(o => o.Family.PrimaryAddress)
                    .Include(o => o.Family.AlternativeAddress)
                    .Include(o => o.Guarantor.Name)
                    .Where(o => o.UserId == Uid)
                    .ToListAsync();

                foreach (var orphan in orphans)
                {
                    var orphanTofill = orphan;
                    _selfLoopBlocking.BlockOrphanSelfLoop(ref orphanTofill);
                    _uriGenerator.SetOrphanUris(ref orphanTofill);
                    orphansList.Add(orphanTofill);
                }
            }
            return orphansList;
        }

        public async Task<int> GetOrphansCount(int Uid)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int orphansCount = await _orphanageDBC.Orphans.AsNoTracking()
                    .Where(o=>o.UserId==Uid)
                    .CountAsync();
                return orphansCount;
            }
        }

        public async Task<OrphanageDataModel.Persons.User> GetUser(int Uid)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var user = await _orphanageDBC.Users.AsNoTracking()
                    .Include(u => u.Name)
                    .Include(u => u.Address)
                    .FirstOrDefaultAsync(o => o.Id == Uid);

                _selfLoopBlocking.BlockUserSelfLoop(ref user);
                return user;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.User>> GetUsers(int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.User> usersList = new List<OrphanageDataModel.Persons.User>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int usersCount = await _orphanageDBC.Users.AsNoTracking().CountAsync();
                if (usersCount < totalSkiped)
                {
                    totalSkiped = usersCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var users = await _orphanageDBC.Users.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(u => u.Name)
                    .Include(u => u.Address)
                    .ToListAsync();

                foreach (var user in users)
                {
                    var userTofill = user;
                    _selfLoopBlocking.BlockUserSelfLoop(ref userTofill);
                    usersList.Add(userTofill);
                }
            }
            return usersList;
        }

        public async Task<int> GetUsersCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int usersCount = await _orphanageDBC.Users.AsNoTracking()
                    .CountAsync();
                return usersCount;
            }
        }
    }
}
