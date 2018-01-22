using OrphanageService.DataContext.Persons;
using OrphanageService.DataContext.RegularData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
