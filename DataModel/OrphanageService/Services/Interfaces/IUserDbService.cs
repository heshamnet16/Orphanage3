using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IUserDbService
    {
        Task<UserDto> GetUserDto(int Uid);

        Task<IEnumerable<UserDto>> GetUsers(int pageSize, int pageNum);

        Task<int> GetUsersCount();

        Task<IEnumerable<BailDto>> GetBails(int Uid);

        Task<IEnumerable<BailDto>> GetBails(int Uid, int pageSize, int pageNum);

        Task<int> GetBailsCount(int Uid);

        Task<IEnumerable<GuarantorDto>> GetGuarantors(int Uid);

        Task<IEnumerable<GuarantorDto>> GetGuarantors(int Uid, int pageSize, int pageNum);

        Task<int> GetGuarantorsCount(int Uid);

        Task<IEnumerable<AccountDto>> GetAccounts(int Uid);

        Task<IEnumerable<AccountDto>> GetAccounts(int Uid, int pageSize, int pageNum);

        Task<int> GetAccountsCount(int Uid);

        Task<IEnumerable<FatherDto>> GetFathers(int Uid);

        Task<IEnumerable<FatherDto>> GetFathers(int Uid, int pageSize, int pageNum);

        Task<int> GetFathersCount(int Uid);

        Task<IEnumerable<MotherDto>> GetMothers(int Uid);

        Task<IEnumerable<MotherDto>> GetMothers(int Uid, int pageSize, int pageNum);

        Task<int> GetMothersCount(int Uid);

        Task<IEnumerable<OrphanDto>> GetOrphans(int Uid);

        Task<IEnumerable<OrphanDto>> GetOrphans(int Uid, int pageSize, int pageNum);

        Task<int> GetOrphansCount(int Uid);

        Task<IEnumerable<CaregiverDto>> GetCaregivers(int Uid);

        Task<IEnumerable<CaregiverDto>> GetCaregivers(int Uid, int pageSize, int pageNum);

        Task<int> GetCaregiversCount(int Uid);

        Task<IEnumerable<FamilyDto>> GetFamilies(int Uid);

        Task<IEnumerable<FamilyDto>> GetFamilies(int Uid, int pageSize, int pageNum);

        Task<int> GetFamiliesCount(int Uid);
    }
}
