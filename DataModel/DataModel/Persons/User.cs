using OrphanageDataModel.FinancialData;
using OrphanageDataModel.RegularData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrphanageDataModel.Persons
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            Bails = new HashSet<Bail>();
            Caregivers = new HashSet<Caregiver>();
            Credits = new HashSet<Credit>();
            Famlies = new HashSet<Family>();
            Fathers = new HashSet<Father>();
            Mothers = new HashSet<Mother>();
            Orphans = new HashSet<Orphan>();
            Guarantors = new HashSet<Guarantor>();
        }

        [Key]
        [Column("ID", TypeName = "int")]
        public int Id { get; set; }


        [Column("Name_ID",TypeName ="int")]
        public int? NameId { get; set; }



        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string UserName { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [MinLength(4, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string Password { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool CanDelete { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool CanRead { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool CanAdd { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool CanDeposit { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool CanDraw { get; set; }

        [Column("Address_ID",TypeName ="int")]
        public int? AddressId { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsAdmin { get; set; }

        public string Note { get; set; }



        public virtual ICollection<Bail> Bails { get; set; }

        public virtual ICollection<Caregiver> Caregivers { get; set; }

        public virtual ICollection<Credit> Credits { get; set; }

        public virtual ICollection<Family> Famlies { get; set; }

        public virtual ICollection<Father> Fathers { get; set; }

        public virtual ICollection<Mother> Mothers { get; set; }

        public virtual Address Address { get; set; }
        public virtual Name Name { get; set; }

        public virtual ICollection<Orphan> Orphans { get; set; }

        public virtual ICollection<Guarantor> Guarantors { get; set; }

    }
}
