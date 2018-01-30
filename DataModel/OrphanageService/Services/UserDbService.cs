using OrphanageService.DataContext;
using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;

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

        public async Task<IEnumerable<AccountDto>> GetAccounts(int Uid)
        {
            IList<AccountDto> accountsList = new List<AccountDto>();
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
                    AccountDto bailDto = Mapper.Map<AccountDto>(accountToFill);
                    accountsList.Add(bailDto);
                }
            }
            return accountsList;
        }

        public async Task<IEnumerable<AccountDto>> GetAccounts(int Uid, int pageSize, int pageNum)
        {
            IList<AccountDto> accountsList = new List<AccountDto>();
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
                    AccountDto bailDto = Mapper.Map<AccountDto>(accountToFill);
                    accountsList.Add(bailDto);
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

        public async Task<IEnumerable<BailDto>> GetBails(int Uid)
        {
            IList<BailDto> bailsList = new List<BailDto>();
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
                    BailDto bailDto = Mapper.Map<BailDto>(bailsToFill);
                    bailsList.Add(bailDto);
                }
            }
            return bailsList;
        }

        public async Task<IEnumerable<BailDto>> GetBails(int Uid, int pageSize, int pageNum)
        {
            IList<BailDto> bailsList = new List<BailDto>();
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
                    BailDto bailDto = Mapper.Map<BailDto>(bailsToFill);
                    bailsList.Add(bailDto);
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

        public async Task<IEnumerable<CaregiverDto>> GetCaregivers(int Uid)
        {
            IList<CaregiverDto> caregiversList = new List<CaregiverDto>();
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
                    CaregiverDto caregiverDC = Mapper.Map<CaregiverDto>(caregiverToFill);
                    _uriGenerator.SetCaregiverUris(ref caregiverDC);
                    caregiversList.Add(caregiverDC);
                }
            }
            return caregiversList;
        }

        public async Task<IEnumerable<CaregiverDto>> GetCaregivers(int Uid, int pageSize, int pageNum)
        {
            IList<CaregiverDto> caregiversList = new List<CaregiverDto>();
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
                    CaregiverDto caregiverDC = Mapper.Map<CaregiverDto>(caregiverToFill);
                    _uriGenerator.SetCaregiverUris(ref caregiverDC);
                    caregiversList.Add(caregiverDC);
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

        public async Task<IEnumerable<FamilyDto>> GetFamilies(int Uid)
        {
            IList<FamilyDto> familiesList = new List<FamilyDto>();
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
                    FamilyDto familyDto = Mapper.Map<FamilyDto>(familyToFill);
                    _uriGenerator.SetFamilyUris(ref familyDto);
                    familiesList.Add(familyDto);
                }
            }
            return familiesList;
        }

        public async Task<IEnumerable<FamilyDto>> GetFamilies(int Uid, int pageSize, int pageNum)
        {
            IList<FamilyDto> familiesList = new List<FamilyDto>();
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
                    FamilyDto familyDto = Mapper.Map<FamilyDto>(familyToFill);
                    _uriGenerator.SetFamilyUris(ref familyDto);
                    familiesList.Add(familyDto);
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

        public async Task<IEnumerable<FatherDto>> GetFathers(int Uid)
        {
            IList<FatherDto> fathersList = new List<FatherDto>();
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
                    FatherDto fatherDC = Mapper.Map<FatherDto>(fatherToFill);
                    _uriGenerator.SetFatherUris(ref fatherDC);
                    fathersList.Add(fatherDC);
                }
            }
            return fathersList;
        }

        public async Task<IEnumerable<FatherDto>> GetFathers(int Uid, int pageSize, int pageNum)
        {
            IList<FatherDto> fathersList = new List<FatherDto>();
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
                    FatherDto fatherDC = Mapper.Map<FatherDto>(fatherToFill);
                    _uriGenerator.SetFatherUris(ref fatherDC);
                    fathersList.Add(fatherDC);
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

        public async Task<IEnumerable<GuarantorDto>> GetGuarantors(int Uid)
        {
            IList<GuarantorDto> guarantorsList = new List<GuarantorDto>();
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
                    GuarantorDto guarantorDto = Mapper.Map<GuarantorDto>(guarantorToFill);
                    guarantorsList.Add(guarantorDto);
                }
            }
            return guarantorsList;
        }

        public async Task<IEnumerable<GuarantorDto>> GetGuarantors(int Uid, int pageSize, int pageNum)
        {
            IList<GuarantorDto> guarantorsList = new List<GuarantorDto>();
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
                    GuarantorDto guarantorDto = Mapper.Map<GuarantorDto>(guarantorToFill);
                    guarantorsList.Add(guarantorDto);
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

        public async Task<IEnumerable<MotherDto>> GetMothers(int Uid)
        {
            IList<MotherDto> mothersList = new List<MotherDto>();
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
                    MotherDto motherDC = Mapper.Map<MotherDto>(motherToFill);
                    _uriGenerator.SetMotherUris(ref motherDC);
                    mothersList.Add(motherDC);
                }
            }
            return mothersList;
        }

        public async Task<IEnumerable<MotherDto>> GetMothers(int Uid, int pageSize, int pageNum)
        {
            IList<MotherDto> mothersList = new List<MotherDto>();
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
                    MotherDto motherDC = Mapper.Map<MotherDto>(motherToFill);
                    _uriGenerator.SetMotherUris(ref motherDC);
                    mothersList.Add(motherDC);
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

        public async Task<IEnumerable<OrphanDto>> GetOrphans(int Uid)
        {
            IList<OrphanDto> orphansList = new List<OrphanDto>();
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
                    OrphanDto orphanDto = Mapper.Map<OrphanDto>(orphanTofill);
                    _uriGenerator.SetOrphanUris(ref orphanDto);
                    orphansList.Add(orphanDto);
                }
            }
            return orphansList;
        }

        public async Task<IEnumerable<OrphanDto>> GetOrphans(int Uid, int pageSize, int pageNum)
        {
            IList<OrphanDto> orphansList = new List<OrphanDto>();
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
                    OrphanDto orphanDto = Mapper.Map<OrphanDto>(orphanTofill);
                    _uriGenerator.SetOrphanUris(ref orphanDto);
                    orphansList.Add(orphanDto);
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

        public async Task<UserDto> GetUserDto(int Uid)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var user = await _orphanageDBC.Users.AsNoTracking()
                    .Include(u => u.Name)
                    .Include(u => u.Address)
                    .FirstOrDefaultAsync(o => o.Id == Uid);

                _selfLoopBlocking.BlockUserSelfLoop(ref user);
                UserDto userDto = Mapper.Map<UserDto>(user);
                return userDto;
            }
        }

        public async Task<IEnumerable<UserDto>> GetUsers(int pageSize, int pageNum)
        {
            IList<UserDto> usersList = new List<UserDto>();
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
                    UserDto orphanDto = Mapper.Map<UserDto>(userTofill);
                    usersList.Add(orphanDto);
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
