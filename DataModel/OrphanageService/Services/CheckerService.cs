using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.DataModel;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class CheckerService : ICheckerService
    {
        private readonly IStringsFixer _stringsFixer;

        public CheckerService(IStringsFixer stringsFixer)
        {
            _stringsFixer = stringsFixer;
        }

        private static CheckerResultData getAddressOwner(Address address)
        {
            if (address.Caregivers?.FirstOrDefault() != null)
            {
                return new CheckerResultData()
                {
                    DataValue = address.Caregivers.FirstOrDefault(),
                    Id = address.Caregivers.FirstOrDefault().Id,
                    ObjectType = typeof(OrphanageDataModel.Persons.Caregiver),
                    DataType = DataTypeEnum.Caregiver
                };
            }
            else if (address.Families?.FirstOrDefault() != null)
            {
                return new CheckerResultData()
                {
                    DataValue = address.Families.FirstOrDefault(),
                    Id = address.Families.FirstOrDefault().Id,
                    ObjectType = typeof(OrphanageDataModel.RegularData.Family),
                    DataType = DataTypeEnum.Family
                };
            }
            else if (address.FamliesAlternativeAddresses?.FirstOrDefault() != null)
            {
                return new CheckerResultData()
                {
                    DataValue = address.FamliesAlternativeAddresses.FirstOrDefault(),
                    Id = address.FamliesAlternativeAddresses.FirstOrDefault().Id,
                    ObjectType = typeof(OrphanageDataModel.RegularData.Family),
                    DataType = DataTypeEnum.Family
                };
            }
            else if (address.Guarantors?.FirstOrDefault() != null)
            {
                return new CheckerResultData()
                {
                    DataValue = address.Guarantors.FirstOrDefault(),
                    Id = address.Guarantors.FirstOrDefault().Id,
                    ObjectType = typeof(OrphanageDataModel.Persons.Guarantor),
                    DataType = DataTypeEnum.Guarantor
                };
            }
            else if (address.Mothers?.FirstOrDefault() != null)
            {
                return new CheckerResultData()
                {
                    DataValue = address.Mothers.FirstOrDefault(),
                    Id = address.Mothers.FirstOrDefault().Id,
                    ObjectType = typeof(OrphanageDataModel.Persons.Mother),
                    DataType = DataTypeEnum.Mother
                };
            }
            else if (address.Users?.FirstOrDefault() != null)
            {
                return new CheckerResultData()
                {
                    DataValue = address.Users.FirstOrDefault(),
                    Id = address.Users.FirstOrDefault().Id,
                    ObjectType = typeof(OrphanageDataModel.Persons.User),
                    DataType = DataTypeEnum.User
                };
            }
            else
            {
                return new CheckerResultData()
                {
                    DataValue = null,
                    Id = -1,
                    DataType = DataTypeEnum.Unknown
                };
            }
        }

        public async Task<CheckerResultData> IsContactDataExist(Address address, OrphanageDbCNoBinary orphanageDbCNo)
        {
            var addresses = await orphanageDbCNo.Addresses
                .Include(a => a.Caregivers)
                .Include(a => a.Families)
                .Include(a => a.FamliesAlternativeAddresses)
                .Include(a => a.Guarantors)
                .Include(a => a.Mothers)
                .Include(a => a.Users)
                .ToListAsync();
            var cellPhone = addresses.Where(a =>a.CellPhone != null &&
                    _stringsFixer.FixPhoneNumber(a.CellPhone) == _stringsFixer.FixPhoneNumber(address.CellPhone)).FirstOrDefault();

            var facebook = addresses.Where(a => a.Facebook != null && a.Facebook == address.Facebook).FirstOrDefault();

            var homephone = addresses.Where(a => a.HomePhone != null && _stringsFixer.FixPhoneNumber(a.HomePhone) == _stringsFixer.FixPhoneNumber(address.HomePhone)).FirstOrDefault();

            var twitter = addresses.Where(a => a.Twitter != null && a.Twitter == address.Twitter).FirstOrDefault();

            var workPhone= addresses.Where(a => a.WorkPhone != null && _stringsFixer.FixPhoneNumber(a.WorkPhone) == _stringsFixer.FixPhoneNumber(address.WorkPhone)).FirstOrDefault();

            var fax = addresses.Where(a => a.Fax != null && _stringsFixer.FixPhoneNumber(a.Fax) == _stringsFixer.FixPhoneNumber(address.Fax)).FirstOrDefault();

            var email = addresses.Where(a => a.Email != null && a.Email == address.Email).FirstOrDefault();

            if (cellPhone != null)
            {
                return getAddressOwner(cellPhone);
            }
            else if (facebook != null)
            {
                return getAddressOwner(facebook);
            }
            else if (homephone != null)
            {
                return getAddressOwner(homephone);
            }
            else if (twitter != null)
            {
                return getAddressOwner(twitter);
            }
            else if (workPhone != null)
            {
                return getAddressOwner(workPhone);
            }
            else if (fax != null)
            {
                return getAddressOwner(fax);
            }
            else if (email != null)
            {
                return getAddressOwner(email);
            }
            else
            {
                return null;
            }
        }

        public async Task<CheckerResultData<OrphanageDataModel.RegularData.Family>> IsFamilyCardNumberExist(string cardNumber, OrphanageDbCNoBinary orphanageDbCNo)
        {
            var fams = await orphanageDbCNo.Families.ToListAsync();
            var fam = fams.Where(f => _stringsFixer.FixPhoneNumber(f.FamilyCardNumber) == _stringsFixer.FixPhoneNumber(cardNumber)).FirstOrDefault();
            if (fam != null)
            {
                return new CheckerResultData<OrphanageDataModel.RegularData.Family>()
                {
                    DataType = DataTypeEnum.Family,
                    Id = fam.Id,
                    DataValue = fam
                };
            }
            return null;
        }

        public async Task<CheckerResultData> IsIdentityNumberExist(string IdNumber, OrphanageDbCNoBinary orphanageDbCNo)
        {
            var mothers = await orphanageDbCNo.Mothers.ToListAsync();
            var mother = mothers.Where (m => m.IdentityCardNumber != null && _stringsFixer.FixIdentityCardNumber(m.IdentityCardNumber) == _stringsFixer.FixIdentityCardNumber(IdNumber)).FirstOrDefault();
            var fathers = await orphanageDbCNo.Fathers.ToListAsync();
            var father = fathers.Where(m => _stringsFixer.FixIdentityCardNumber(m.IdentityCardNumber) == _stringsFixer.FixIdentityCardNumber(IdNumber)).FirstOrDefault();
            var orphans = await orphanageDbCNo.Orphans.ToListAsync();
            var orphan = orphans.Where(m => _stringsFixer.FixIdentityCardNumber(m.IdentityCardNumber) == _stringsFixer.FixIdentityCardNumber(IdNumber)).FirstOrDefault();
            var caregivers = await orphanageDbCNo.Caregivers.ToListAsync();
            var caregiver = caregivers.Where(m => _stringsFixer.FixIdentityCardNumber(m.IdentityCardId) == _stringsFixer.FixIdentityCardNumber(IdNumber)).FirstOrDefault();

            if (mother != null)
            {
                return new CheckerResultData()
                {
                    DataType = DataTypeEnum.Mother,
                    DataValue = mother,
                    ObjectType = typeof(OrphanageDataModel.Persons.Mother),
                    Id = mother.Id
                };
            }
            if (father != null)
            {
                return new CheckerResultData()
                {
                    DataType = DataTypeEnum.Father,
                    DataValue = father,
                    ObjectType = typeof(OrphanageDataModel.Persons.Father),
                    Id = father.Id
                };
            }
            if (caregiver != null)
            {
                return new CheckerResultData()
                {
                    DataType = DataTypeEnum.Caregiver,
                    DataValue = caregiver,
                    ObjectType = typeof(OrphanageDataModel.Persons.Caregiver),
                    Id = caregiver.Id
                };
            }
            if (orphan != null)
            {
                return new CheckerResultData()
                {
                    DataType = DataTypeEnum.Orphan,
                    DataValue = orphan,
                    ObjectType = typeof(OrphanageDataModel.Persons.Orphan),
                    Id = orphan.Id
                };
            }
            return null;
        }

        public async Task<CheckerResultData> IsNameExist(Name name, OrphanageDbCNoBinary orphanageDbCNo)
        {
            var datas = await orphanageDbCNo.Names
                .Include(n => n.Caregivers)
                .Include(n => n.Fathers)
                .Include(n => n.Guarantors)
                .Include(n => n.Mothers)
                .Include(n => n.Orphans).ToListAsync();
            var data = datas
                .Where(
                n => (_stringsFixer.FixArabicNames(n.First) == _stringsFixer.FixArabicNames(name.First)) &&
                (_stringsFixer.FixArabicNames(n.Father) == _stringsFixer.FixArabicNames(name.Father)) &&
                (_stringsFixer.FixArabicNames(n.Last) == _stringsFixer.FixArabicNames(name.Last))).FirstOrDefault();

            if (data?.Caregivers?.FirstOrDefault() != null)
            {
                return new CheckerResultData()
                {
                    Id = data.Caregivers.FirstOrDefault().Id,
                    DataType = DataTypeEnum.Caregiver,
                    ObjectType = typeof(OrphanageDataModel.Persons.Caregiver),
                    DataValue = data.Caregivers.FirstOrDefault()
                };
            }
            else if (data?.Fathers?.FirstOrDefault() != null)
            {
                return new CheckerResultData()
                {
                    Id = data.Fathers.FirstOrDefault().Id,
                    DataType = DataTypeEnum.Father,
                    ObjectType = typeof(OrphanageDataModel.Persons.Father),
                    DataValue = data.Fathers.FirstOrDefault()
                };
            }
            else if (data?.Guarantors?.FirstOrDefault() != null)
            {
                return new CheckerResultData()
                {
                    Id = data.Guarantors.FirstOrDefault().Id,
                    DataType = DataTypeEnum.Guarantor,
                    ObjectType = typeof(OrphanageDataModel.Persons.Guarantor),
                    DataValue = data.Guarantors.FirstOrDefault()
                };
            }
            else if (data?.Mothers?.FirstOrDefault() != null)
            {
                return new CheckerResultData()
                {
                    Id = data.Mothers.FirstOrDefault().Id,
                    DataType = DataTypeEnum.Mother,
                    ObjectType = typeof(OrphanageDataModel.Persons.Mother),
                    DataValue = data.Mothers.FirstOrDefault()
                };
            }
            else if (data?.Orphans?.FirstOrDefault() != null)
            {
                return new CheckerResultData()
                {
                    Id = data.Orphans.FirstOrDefault().Id,
                    DataType = DataTypeEnum.Orphan,
                    ObjectType = typeof(OrphanageDataModel.Persons.Orphan),
                    DataValue = data.Orphans.FirstOrDefault()
                };
            }
            else
            {
                return null;
            }
        }
    }
}