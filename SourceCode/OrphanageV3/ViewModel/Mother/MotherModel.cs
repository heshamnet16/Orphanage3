using System;

namespace OrphanageV3.ViewModel.Mother
{
    public class MotherModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }

        public string HusbandsNames { get; set; }

        public DateTime Birthday { get; set; }

        public bool IsDead { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public bool IsMarried { get; set; }

        public string HusbandName { get; set; }

        public bool? HasSheOrphans { get; set; }

        public string IdentityCardNumber { get; set; }

        public string Jop { get; set; }

        public decimal? Income { get; set; }

        public long? ColorMark { get; set; }

        public string Story { get; set; }

        public string FullAddress { get; set; }

        public string CellPhone { get; set; }

        public string HomePhone { get; set; }

        public string WorkPhone { get; set; }

        public DateTime RegDate { get; set; }

        public string UserName { get; set; }

        public string Notes { get; set; }

        public string IdentityCardImageFaceURI { get; set; }

        public string IdentityCardImageBackURI { get; set; }

        public int OrphansCount { get; set; }
    }
}