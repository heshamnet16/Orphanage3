﻿using OrphanageDataModel.Persons;
using OrphanageDataModel.RegularData;
using OrphanageDataModel.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrphanageDataModel.FinancialData
{
    [Table("Bails")]
    public class Bail
    {
        public Bail()
        {
            RegDate = DateTime.Now;
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Supporter_ID")]
        public int GuarantorID { get; set; }

        public virtual Guarantor Guarantor { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public decimal Amount { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Column("Box_ID")]
        public int AccountID { get; set; }

        public virtual Account Account { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Column("Is_Family")]
        public bool IsFamilyBail { get; set; }

        [Column("Start_Date")]
        public DateTime? StartDate { get; set; }

        [Column("End_Date")]
        [BailEndDateValidator]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Column("Is_Ended")]
        public bool IsExpired { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Column("IsMonthly")]
        public bool IsMonthlyBail { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public DateTime RegDate { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Column("User_ID")]
        public int UserId { get; set; }

        public virtual User ActingUser { get; set; }

        public string Note { get; set; }

        [NotMapped]
        public int OrphansCount { get; set; }

        [NotMapped]
        public int FamiliesCount { get; set; }

        public virtual ICollection<Orphan> Orphans { get; set; }

        public virtual ICollection<Family> Families { get; set; }
    }
}