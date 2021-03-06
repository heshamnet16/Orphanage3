﻿using OrphanageDataModel.FinancialData;
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
            RegDate = DateTime.Now;
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Name_ID")]
        public int? NameId { get; set; }

        public virtual Name Name { get; set; }

        [StringLength(450)]
        [Index(IsUnique = true)]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [MinLength(4, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public bool CanDelete { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public bool CanRead { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public bool CanAdd { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public bool CanDeposit { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public bool CanDraw { get; set; }

        [Column("Address_ID")]
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public DateTime RegDate { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
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