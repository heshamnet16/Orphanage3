using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
using OrphanageService.Utilities.Interfaces;
using System;

namespace OrphanageService.Utilities
{
    public class UriGenerator : IUriGenerator
    {
        public void SetCaregiverUris(ref CaregiverDC orphanDC)
        {
            throw new NotImplementedException();
        }

        public void SetFamilyUris(ref FamilyDC familyDC)
        {
            familyDC.FamilyCardImagePage1URI = "api/family/media/page1/" + familyDC.Id;
            familyDC.FamilyCardImagePage2URI = "api/family/media/page2/" + familyDC.Id;
        }

        public void SetFatherUris(ref FatherDC fatherDC)
        {
            fatherDC.PersonalPhotoURI = "api/father/media/face/" + fatherDC.Id;
            fatherDC.DeathCertificateImageURI = "api/father/media/death/" + fatherDC.Id;
        }

        public void SetMotherUris(ref MotherDC motherDC)
        {
            motherDC.IdentityCardFaceURI = "api/mother/media/idface/" + motherDC.Id;
            motherDC.IdentityCardBackURI = "api/mother/media/idback/" + motherDC.Id;
        }

        public void SetOrphanUris(ref OrphanDC orphanDC)
        {
            orphanDC.FacePhotoURI = "api/orphan/media/face/" + orphanDC.Id;
            orphanDC.BirthCertificatePhotoURI = "api/orphan/media/birth/" + orphanDC.Id;
            orphanDC.FamilyCardPagePhotoURI = "api/orphan/media/familycard/" + orphanDC.Id;
            orphanDC.FullPhotoURI = "api/orphan/media/full/" + orphanDC.Id;
        }
    }
}
