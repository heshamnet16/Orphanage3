using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;

namespace OrphanageService.Utilities.Interfaces
{
    public interface IUriGenerator
    {
        void SetOrphanUris(ref OrphanDto orphanDC);
        void SetFatherUris(ref FatherDto fatherDC);
        void SetMotherUris(ref MotherDto motherDC);
        void SetFamilyUris(ref FamilyDto familyDC);
        void SetCaregiverUris(ref CaregiverDto caregiverDC);
    }
}
