using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Caregiver
{
    public class CaregiverModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }

        public string FullAddress { get; set; }

        public string CellPhone { get; set; }

        public string HomePhone { get; set; }

        public string WorkPhone { get; set; }

        public string IdentityCardNumber { get; set; }

        public string Jop { get; set; }

        public decimal? Income { get; set; }

        public long? ColorMark { get; set; }

        public DateTime RegDate { get; set; }

        public string UserName  { get; set; }

        public string Notes { get; set; }

        public string IdentityCardImageFaceURI { get; set; }

        public string IdentityCardImageBackURI { get; set; }

        public int OrphansCount { get; set; }

    }
}
