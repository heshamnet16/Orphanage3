namespace OrphanageService.Utilities.Interfaces
{
    public interface IUriGenerator
    {
        void SetOrphanUris(ref OrphanageDataModel.Persons.Orphan orphanDC);

        void SetFatherUris(ref OrphanageDataModel.Persons.Father fatherDC);

        void SetMotherUris(ref OrphanageDataModel.Persons.Mother motherDC);

        void SetFamilyUris(ref OrphanageDataModel.RegularData.Family familyDC);

        void SetCaregiverUris(ref OrphanageDataModel.Persons.Caregiver caregiverDC);
    }
}