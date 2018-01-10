using OrphanageDataModel.Persons;
using OrphanageDataModel.FinancialData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrphanageDataModel.FinancialData
{
    [Table("Bails")]
    public class Bail
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }


        [Column("Supporter_ID")]
        [ForeignKey("Guarantor")]
        public int? GuarantorID { get; set; }
        public virtual Guarantor Guarantor { get; set; }



        [Required(ErrorMessageResourceName ="ErrorRequired",ErrorMessageResourceType = typeof(string))]
        public decimal Amount { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("Box_ID")]
        [ForeignKey("Account")]
        public int CreditID { get; set; }
        public virtual Credit Account { get; set; }



        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("Is_Family")]
        public bool IsFamilyBail { get; set; }


        [Column("Start_Date")]
        public DateTime? StartDate { get; set; }


        [Column("End_Date")]
        public DateTime? EndDate { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("Is_Ended")]
        public bool IsExpired { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("IsMonthly")]
        public bool IsMonthlyBail { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("User_ID")]
        [ForeignKey("ActingUser")]
        public int UserId { get; set; }
        public virtual User ActingUser { get; set; }


        public string Note { get; set; }



    }
}
