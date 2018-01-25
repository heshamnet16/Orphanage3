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
            if (familyDC == null) return;
            familyDC.FamilyCardImagePage1URI = "api/family/media/page1/" + familyDC.Id;
            familyDC.FamilyCardImagePage2URI = "api/family/media/page2/" + familyDC.Id;
            if(familyDC.Father != null)
            {
                var fatherDc = familyDC.Father;
                SetFatherUris(ref fatherDc);
                familyDC.Father = fatherDc;
            }
            if (familyDC.Mother != null)
            {
                var motherDc = familyDC.Mother;
                SetMotherUris(ref motherDc);
                familyDC.Mother = motherDc;
            }
        }

        public void SetFatherUris(ref FatherDC fatherDC)
        {
            if (fatherDC == null) return;
            fatherDC.PersonalPhotoURI = "api/father/media/face/" + fatherDC.Id;
            fatherDC.DeathCertificateImageURI = "api/father/media/death/" + fatherDC.Id;
            if(fatherDC.Families != null)
                foreach(var fam in fatherDC.Families)
                {
                    var familyDC = fam;
                    SetFamilyUris(ref familyDC);
                    fatherDC.Families[fatherDC.Families.IndexOf(fam)] = familyDC;
                }
        }

        public void SetMotherUris(ref MotherDC motherDC)
        {
            if (motherDC == null) return;
            motherDC.IdentityCardFaceURI = "api/mother/media/idface/" + motherDC.Id;
            motherDC.IdentityCardBackURI = "api/mother/media/idback/" + motherDC.Id;
            if (motherDC.Families != null)
                foreach (var fam in motherDC.Families)
                {
                    var familyDC = fam;
                    SetFamilyUris(ref familyDC);
                    motherDC.Families[motherDC.Families.IndexOf(fam)] = familyDC;
                }
        }

        public void SetOrphanUris(ref OrphanDC orphanDC)
        {
            if (orphanDC == null) return;
            orphanDC.FacePhotoURI = "api/orphan/media/face/" + orphanDC.Id;
            orphanDC.BirthCertificatePhotoURI = "api/orphan/media/birth/" + orphanDC.Id;
            orphanDC.FamilyCardPagePhotoURI = "api/orphan/media/familycard/" + orphanDC.Id;
            orphanDC.FullPhotoURI = "api/orphan/media/full/" + orphanDC.Id;
            if (orphanDC.Caregiver != null)
            {
                CaregiverDC caregiverDC = orphanDC.Caregiver;
                //TODO  #11
                //SetCaregiverUris(ref caregiverDC);
                orphanDC.Caregiver = caregiverDC;
            }
            if(orphanDC.Education !=null)
            {
                orphanDC.Education.CertificateImageURI = "api/orphan/media/education/"+ orphanDC.Id;
                orphanDC.Education.CertificateImage2URI = "api/orphan/media/education2/" + orphanDC.Id;
            }
            if (orphanDC.HealthStatus != null)
            {
                orphanDC.HealthStatus.ReporteFileURI = "api/orphan/media/healthreport/" + orphanDC.Id;
            }
            if (orphanDC.Family != null)
            {
                var familyDC = orphanDC.Family;
                SetFamilyUris(ref familyDC);
                orphanDC.Family = familyDC;
            }

        }
    }
}
