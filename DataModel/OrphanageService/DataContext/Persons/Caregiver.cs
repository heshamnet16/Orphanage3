using OrphanageService.DataContext.RegularData;
using System;
using System.Collections.Generic;

namespace OrphanageService.DataContext.Persons
{
    public class CaregiverDC
    {
        public int Id { get; set; }

        public int NameId { get; set; }

        public virtual NameDC Name { get; set; }

        public int? AddressId { get; set; }

        public virtual AddressDC Address { get; set; }

        public string IdentityCardId { get; set; }
        
        public string Jop { get; set; }

        public decimal? Income { get; set; }

        public long? ColorMark { get; set; }

        public DateTime RegDate { get; set; }

        public int UserId { get; set; }

        public virtual UserDC ActingUser { get; set; }

        public string Note { get; set; }
        
        public string IdentityCardImageFaceURI { get; set; }

        public string IdentityCardImageBackURI { get; set; }

        public virtual ICollection<OrphanDC> Orphans { get; set; }

    }
}
