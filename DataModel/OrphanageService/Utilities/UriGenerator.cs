using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
using OrphanageService.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Utilities
{
    public class UriGenerator : IUriGenerator
    {
        public void SetCaregiverUris(ref CaregiverDC orphanDC)
        {
            throw new NotImplementedException();
        }

        public void SetFamilyUris(ref FamilyDC orphanDC)
        {
            throw new NotImplementedException();
        }

        public void SetFatherUris(ref FatherDC fatherDC)
        {
            fatherDC.PersonalPhotoURI = "api/father/media/face/" + fatherDC.Id;
            fatherDC.DeathCertificateImageURI = "api/father/media/death/" + fatherDC.Id;
        }

        public void SetMotherUris(ref MotherDC orphanDC)
        {
            throw new NotImplementedException();
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
