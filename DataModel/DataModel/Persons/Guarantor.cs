using OrphanageDataModel.RegularData;
using OrphanageDataModel.FinancialData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace OrphanageDataModel.Persons
{
    [Table("Supporters")]
    public class Guarantor
    {
        public Guarantor()
        {
            Bails = new HashSet<Bail>();
            Orphans = new HashSet<Orphan>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Name_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Name")]
        public int NameId { get; set; }
        public virtual Name Name { get; set; }


        [Column("Address_ID")]
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }


        [Column("Box_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Account")]
        public int CreditId { get; set; }
        public virtual Account Account { get; set; }


        [Column("Color_Mark")]
        public long? ColorMark { get; set; }

        [Column("Is_Family_sup")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsSupportingTheWholeFamily { get; set; }

        [Column("Is_Monthly_sup")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsPayingMonthly { get; set; }

        [Column("Is_Still_Suppo")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsStillPaying { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }


        [Column("User_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("ActingUser")]
        public int UserId { get; set; }
        public virtual User ActingUser { get; set; }


        public string Note { get; set; }



        public virtual ICollection<Bail> Bails { get; set; }

        public virtual ICollection<Orphan> Orphans { get; set; }


    }
}
