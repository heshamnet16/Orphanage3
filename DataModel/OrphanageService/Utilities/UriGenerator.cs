using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;

namespace OrphanageService.Utilities
{
    public class UriGenerator : IUriGenerator
    {
        public void SetCaregiverUris(ref OrphanageDataModel.Persons.Caregiver caregiverDto)
        {
            caregiverDto.IdentityCardImageBackURI = "api/caregiver/media/idback/" + caregiverDto.Id;
            caregiverDto.IdentityCardImageFaceURI = "api/caregiver/media/idface/" + caregiverDto.Id;
            if (caregiverDto.Orphans != null)
            {
                var tempList = new List<OrphanageDataModel.Persons.Orphan>();
                foreach (var orp in caregiverDto.Orphans)
                {
                    if (orp.BirthCertificatePhotoURI != null && orp.BirthCertificatePhotoURI.Length > 0)
                        continue;
                    var orphanDto = orp;
                    SetOrphanUris(ref orphanDto);
                    tempList.Add(orphanDto);
                }
                caregiverDto.Orphans = tempList;
            }
        }

        public void SetFamilyUris(ref OrphanageDataModel.RegularData.Family familyDC)
        {
            if (familyDC == null) return;
            familyDC.FamilyCardImagePage1URI = "api/family/media/page1/" + familyDC.Id;
            familyDC.FamilyCardImagePage2URI = "api/family/media/page2/" + familyDC.Id;
            if (familyDC.Father != null)
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

        public void SetFatherUris(ref OrphanageDataModel.Persons.Father fatherDto)
        {
            if (fatherDto == null) return;
            fatherDto.PersonalPhotoURI = "api/father/media/photo/" + fatherDto.Id;
            fatherDto.DeathCertificateImageURI = "api/father/media/death/" + fatherDto.Id;
            if (fatherDto.Families != null)
            {
                var tempList = new List<OrphanageDataModel.RegularData.Family>();
                foreach (var fam in fatherDto.Families)
                {
                    var familyDC = fam;
                    SetFamilyUris(ref familyDC);
                    tempList.Add(familyDC);
                }
                fatherDto.Families = tempList;
            }
        }

        public void SetMotherUris(ref OrphanageDataModel.Persons.Mother motherDto)
        {
            if (motherDto == null) return;
            motherDto.IdentityCardFaceURI = "api/mother/media/idface/" + motherDto.Id;
            motherDto.IdentityCardBackURI = "api/mother/media/idback/" + motherDto.Id;
            if (motherDto.Families != null)
            {
                var tempList = new List<OrphanageDataModel.RegularData.Family>();
                foreach (var fam in motherDto.Families)
                {
                    var familyDto = fam;
                    SetFamilyUris(ref familyDto);
                    tempList.Add(familyDto);
                }
                motherDto.Families = tempList;
            }
        }

        public void SetOrphanUris(ref OrphanageDataModel.Persons.Orphan orphanDC)
        {
            if (orphanDC == null) return;
            orphanDC.FacePhotoURI = "api/orphan/media/face/" + orphanDC.Id;
            orphanDC.BirthCertificatePhotoURI = "api/orphan/media/birth/" + orphanDC.Id;
            orphanDC.FamilyCardPagePhotoURI = "api/orphan/media/familycard/" + orphanDC.Id;
            orphanDC.FullPhotoURI = "api/orphan/media/full/" + orphanDC.Id;
            if (orphanDC.Caregiver != null)
            {
                OrphanageDataModel.Persons.Caregiver caregiverDC = orphanDC.Caregiver;
                SetCaregiverUris(ref caregiverDC);
                orphanDC.Caregiver = caregiverDC;
            }
            if (orphanDC.Education != null)
            {
                orphanDC.Education.CertificateImageURI = "api/orphan/media/education/" + orphanDC.Id;
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