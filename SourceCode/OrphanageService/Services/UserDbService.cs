using OrphanageDataModel.Persons;
using OrphanageService.DataContext;
using OrphanageService.Services.Exceptions;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class UserDbService : IUserDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;
        private readonly ISecurePasswordHasher _passwordHasher;
        private readonly ILogger _logger;
        private readonly IRegularDataService _regularDataService;

        public UserDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator, ISecurePasswordHasher securePasswordHasher, ILogger logger, IRegularDataService regularDataService)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
            _passwordHasher = securePasswordHasher;
            _logger = logger;
            _regularDataService = regularDataService;
        }

        public async Task<OrphanageDataModel.Persons.User> AddUser(OrphanageDataModel.Persons.User user)
        {
            _logger.Information("trying to add new user");
            if (user == null)
            {
                _logger.Error("user is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            using (var orphanageDBC = new OrphanageDbCNoBinary())
            {
                using (var Dbt = orphanageDBC.Database.BeginTransaction())
                {
                    int ret = 0;
                    if (user.Name != null)
                    {
                        var nameId = await _regularDataService.AddName(user.Name, orphanageDBC);
                        if (nameId > 0)
                        {
                            user.NameId = nameId;
                        }
                        else
                        {
                            Dbt.Rollback();
                            _logger.Warning($"Name object has not been added, nothing will be added, null will be returned");
                            return null;
                        }
                    }
                    if (user.Address != null)
                    {
                        var addressId = await _regularDataService.AddAddress(user.Address, orphanageDBC);
                        if (addressId == -1)
                        {
                            Dbt.Rollback();
                            _logger.Warning($"Address object has not been added, nothing will be added, null will be returned");
                            return null;
                        }
                        user.AddressId = addressId;
                    }
                    user.Accounts = null;
                    user.Bails = null;
                    user.Caregivers = null;
                    user.Famlies = null;
                    user.Fathers = null;
                    user.Guarantors = null;
                    user.Mothers = null;
                    user.Orphans = null;
                    _logger.Information("trying to hash the user password");
                    user.Password = _passwordHasher.Hash(user.Password);
                    orphanageDBC.Users.Add(user);
                    ret = await orphanageDBC.SaveChangesAsync();
                    if (ret >= 1)
                    {
                        Dbt.Commit();
                        _logger.Information($"new user object with id {user.Id} has been added");
                        _logger.Information($"the caregiver object with id {user.Id}  will be returned");
                        return user;
                    }
                    else
                    {
                        Dbt.Rollback();
                        _logger.Warning($"something went wrong, nothing was added, null will be returned");
                        return null;
                    }
                }
            }
        }

        public async Task<OrphanageDataModel.Persons.User> AuthenticateUser(string userName, string password)
        {
            _logger.Information($"trying to authenticate user, username={userName}");
            if (userName == null || userName.Length == 0)
            {
                _logger.Error("username is empty, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (password == null || password.Length == 0)
            {
                _logger.Error("password is empty, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            using (var orphanageDBC = new OrphanageDbCNoBinary())
            {
                var user = await orphanageDBC.Users.FirstOrDefaultAsync(u => u.UserName == userName);
                if (user == null)
                {
                    _logger.Warning($"username is not existed, AuthenticationException will be thrown");
                    throw new AuthenticationException($"username={userName}");
                }
                if (!_passwordHasher.Verify(password, user.Password))
                {
                    _logger.Warning($"password doesn't match, AuthenticationException will be thrown");
                    throw new AuthenticationException($"wrong password");
                }
                _logger.Information($"username={userName} is successfully authenticated");
                return user;
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            _logger.Information($"trying to delete User with id({userId})");
            if (userId <= 0)
            {
                _logger.Error($"the integer parameter User ID is less than zero, false will be returned");
                throw new NullReferenceException();
            }
            var user = await GetUser(userId);
            user.CanAdd = false;
            user.CanDelete = false;
            user.CanDeposit = false;
            user.CanDraw = false;
            user.CanRead = false;
            user.IsAdmin = false;
            var ret = await SaveUser(user);
            if (ret)
            {
                _logger.Information($"User with id({userId}) has been successfully deleted from the database");
                return true;
            }
            else
            {
                _logger.Information($"nothing has changed, false will be returned");
                return false;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Account>> GetAccounts(int Uid)
        {
            IList<OrphanageDataModel.FinancialData.Account> accountsList = new List<OrphanageDataModel.FinancialData.Account>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var accounts = await _orphanageDBC.Accounts.AsNoTracking()
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
                    .Where(a => a.UserId == Uid)
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
                    .Where(b => b.UserId == Uid)
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
                    .Where(b => b.UserId == Uid)
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
                    .Where(b => b.UserId == Uid).CountAsync();
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
                    .Where(f => f.UserId == Uid)
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
                    .Where(f => f.UserId == Uid)
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
                    .Where(g => g.UserId == Uid)
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
                    .Where(m => m.UserId == Uid)
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
                    .Where(o => o.UserId == Uid)
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

                if (user == null) return null;
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

        public async Task<bool> SaveUser(OrphanageDataModel.Persons.User user)
        {
            _logger.Information($"Trying to save user");
            if (user == null)
            {
                _logger.Error($"the parameter object user is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (user.UserName == null || user.UserName.Length == 0)
            {
                _logger.Error($"the UserName of the parameter object user equals {user.UserName}, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            using (OrphanageDbCNoBinary orphanageDc = new OrphanageDbCNoBinary())
            {
                int ret = 0;
                orphanageDc.Configuration.LazyLoadingEnabled = true;
                orphanageDc.Configuration.ProxyCreationEnabled = true;
                orphanageDc.Configuration.AutoDetectChangesEnabled = true;

                var orginalUser = await orphanageDc.Users.
                    Include(m => m.Address).
                    Include(c => c.Name).
                    FirstOrDefaultAsync(m => m.Id == user.Id);

                if (orginalUser == null)
                {
                    _logger.Error($"the original user object with id {user.Id} object is not founded, ObjectNotFoundException will be thrown");
                    throw new Exceptions.ObjectNotFoundException();
                }

                _logger.Information($"processing the address object of the user with id({user.Id})");
                if (user.Address != null)
                    if (orginalUser.Address != null)
                        //edit existing user address
                        ret += await _regularDataService.SaveAddress(user.Address, orphanageDc);
                    else
                    {
                        //create new address for the user
                        var addressId = await _regularDataService.AddAddress(user.Address, orphanageDc);
                        orginalUser.AddressId = addressId;
                        ret++;
                    }
                else
                {
                    if (orginalUser.Address != null)
                    {
                        //delete existing user address
                        int alAdd = orginalUser.AddressId.Value;
                        orginalUser.AddressId = null;
                        await orphanageDc.SaveChangesAsync();
                        await _regularDataService.DeleteAddress(alAdd, orphanageDc);
                    }
                }
                if (user.Name != null)
                    if (orginalUser.Name != null)
                        //edit existing user name
                        ret += await _regularDataService.SaveName(user.Name, orphanageDc);
                    else
                    {
                        //create new name for the user
                        var nameId = await _regularDataService.AddName(user.Name, orphanageDc);
                        orginalUser.NameId = nameId;
                        ret++;
                    }
                else
                {
                    if (orginalUser.Name != null)
                    {
                        //delete existing user name
                        int alAdd = orginalUser.NameId.Value;
                        orginalUser.NameId = null;
                        await orphanageDc.SaveChangesAsync();
                        await _regularDataService.DeleteName(alAdd, orphanageDc);
                    }
                }
                orginalUser.CanAdd = user.CanAdd;
                orginalUser.CanDelete = user.CanDelete;
                orginalUser.CanDeposit = user.CanDeposit;
                orginalUser.CanDraw = user.CanDraw;
                orginalUser.CanRead = user.CanRead;
                orginalUser.IsAdmin = user.IsAdmin;
                orginalUser.Password = _passwordHasher.Hash(user.Password);
                orginalUser.UserName = user.UserName;
                orginalUser.Note = user.Note;
                ret += await orphanageDc.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"user with id({user.Id}) has been successfully saved to the database, {ret} changes have been made");
                    return true;
                }
                else
                {
                    _logger.Information($"nothing has changed, false will be returned");
                    return false;
                }
            }
        }
    }
}