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
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Name_ID")]
        public int? NameId { get; set; }

        public virtual Name Name { get; set; }

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

        [Column("Address_ID")]
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsAdmin { get; set; }

        public string Note { get; set; }

        public virtual ICollection<Bail> Bails { get; set; }

        public virtual ICollection<Caregiver> Caregivers { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<Family> Famlies { get; set; }

        public virtual ICollection<Father> Fathers { get; set; }

        public virtual ICollection<Mother> Mothers { get; set; }

        public virtual ICollection<Orphan> Orphans { get; set; }

        public virtual ICollection<Guarantor> Guarantors { get; set; }
    }
}