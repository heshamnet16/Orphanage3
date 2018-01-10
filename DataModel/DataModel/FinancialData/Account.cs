using OrphanageDataModel.Persons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrphanageDataModel.FinancialData
{
    [Table("Boxes")]
    public class Account
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Name")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public string AccountName { get; set; }


        [Column("Cur_Name")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public string Currency { get; set; }


        [Column("Cur_Short")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public string CurrencyShortcut { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("AMount")]
        public decimal Amount { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("Is_Positive")]
        public bool CanNotBeNegative { get; set; }

        public string Note { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }


        [Column("User_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int UserId { get; set; }
        public virtual User ActingUser { get; set; }


        public virtual ICollection<Bail> Bails { get; set; }
        public virtual ICollection<Guarantor> Guarantors { get; set; }
    }
}
