using OrphanageService.DataContext.RegularData;
using System;
using System.Collections.Generic;

namespace OrphanageService.DataContext.Persons
{
    public class FatherDC
    {
        public int Id { get; set; }

        public int NameId { get; set; }

        public virtual NameDC Name { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime DateOfDeath { get; set; }

        public string Jop { get; set; }

        public string Story { get; set; }

        public string DeathReason { get; set; }

        public long? ColorMark { get; set; }

        public string IdentityCardNumber { get; set; }

        public DateTime RegDate { get; set; }

        public int UserId { get; set; }

        public virtual UserDC ActingUser { get; set; }

        public string Note { get; set; }

        public string PersonalPhotoURI { get; set; }

        public string DeathCertificateImageURI { get; set; }



    }
}
