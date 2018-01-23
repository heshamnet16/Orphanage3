using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;

namespace OrphanageService.Utilities.Interfaces
{
    public interface IUriGenerator
    {
        void SetOrphanUris(ref OrphanDC orphanDC);
        void SetFatherUris(ref FatherDC fatherDC);
        void SetMotherUris(ref MotherDC motherDC);
        void SetFamilyUris(ref FamilyDC familyDC);
        void SetCaregiverUris(ref CaregiverDC caregiverDC);
    }
}
