using OrphanageService.DataContext.Persons;
using System.Collections.Generic;

namespace OrphanageService.DataContext.RegularData
{
    public class NameDC
    {
        public int Id { get; set; }

        public string First { get; set; }

        public string  Father { get; set; }

        public string Last { get; set; }

        public string EnglishFirst { get; set; }

        public string EnglishFather { get; set; }

        public string  EnglishLast { get; set; }

        public virtual ICollection<CaregiverDC> Caregivers { get; set; }

        public virtual ICollection<FatherDC> Fathers { get; set; }

        public virtual ICollection<MotherDC> Mothers { get; set; }

        public virtual ICollection<OrphanDC> Orphans { get; set; }

        public virtual ICollection<GuarantorDC> Guarantors { get; set; }

        public virtual ICollection<UserDC> Users { get; set; }
    }
}
