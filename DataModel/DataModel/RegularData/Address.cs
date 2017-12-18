using OrphanageDataModel.Persons;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrphanageDataModel.RegularData
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string Country { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string City { get; set; }

        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string Town { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string Street { get; set; }

        [RegularExpression(@"\(?\d{3}\)?-?*\d{3}-?*-?\d{4}", ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        [Column("Home_Phone")]
        public string HomePhone { get; set; }


        [RegularExpression(@"\(?\d{3}\)?-?*\d{3}-?*-?\d{4}", ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        [Column("Cell_Phone")]
        public string CellPhone { get; set; }

        [RegularExpression(@"\(?\d{3}\)?-?*\d{3}-?*-?\d{4}", ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        [Column("Work_phone")]
        public string WorkPhone { get; set; }

        [RegularExpression(@"\(?\d{3}\)?-?*\d{3}-?*-?\d{4}", ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        string Fax { get; set; }

        [EmailAddress(ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        string Email { get; set; }


        [RegularExpression(@"(?:(?:http|https):\/\/)?(?:www.)?facebook.com\/(?:(?:\w)*#!\/)?(?:pages\/)?(?:[?\w\-]*\/)?(?:profile.php\?id=(?=\d.*))?([\w\-]*)?",
            ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string Facebook { get; set; }

        [RegularExpression(@"http(?:s)?:\/\/(?:www\.)?twitter\.com\/([a-zA-Z0-9_]+)", ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string Twitter { get; set; }

        public string Note { get; set; }



        public virtual ICollection<Caregiver> Caregivers { get; set; }

        [InverseProperty("PrimaryAddress")]
        public virtual ICollection<Family> Families { get; set; }

        [InverseProperty("AlternativeAddress")]
        public virtual ICollection<Family> FamliesAlternativeAddresses { get; set; }

        public virtual ICollection<Mother> Mothers { get; set; }

        public virtual ICollection<Guarantor> Guarantors { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
