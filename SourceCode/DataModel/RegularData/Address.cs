﻿using OrphanageDataModel.Persons;
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

        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Country { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string City { get; set; }

        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Town { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Street { get; set; }

        [RegularExpression(@"^([(]?\d{3}[)]?[- ]?\d{4}[- ]?\d{3})|([(]?\d{4}[)]?[- ]?\d{3}[- ]?\d{3})$", ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Column("Home_Phone")]
        public string HomePhone { get; set; }

        [RegularExpression(@"^([(]?\d{3}[)]?[- ]?\d{4}[- ]?\d{3})|([(]?\d{4}[)]?[- ]?\d{3}[- ]?\d{3})$", ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Column("Cell_Phone")]
        public string CellPhone { get; set; }

        [RegularExpression(@"^([(]?\d{3}[)]?[- ]?\d{4}[- ]?\d{3})|([(]?\d{4}[)]?[- ]?\d{3}[- ]?\d{3})$", ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Column("Work_phone")]
        public string WorkPhone { get; set; }

        [RegularExpression(@"^([(]?\d{3}[)]?[- ]?\d{4}[- ]?\d{3})|([(]?\d{4}[)]?[- ]?\d{3}[- ]?\d{3})$", ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Fax { get; set; }

        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string Email { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Note { get; set; }

        public virtual ICollection<Caregiver> Caregivers { get; set; }

        public virtual ICollection<Family> Families { get; set; }

        public virtual ICollection<Family> FamliesAlternativeAddresses { get; set; }

        public virtual ICollection<Mother> Mothers { get; set; }

        public virtual ICollection<Guarantor> Guarantors { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public bool Equals(Address second)
        {
            if (second == null || this == null) return false;
            if (CellPhone != null && CellPhone.Length > 0)
            {
                if (StringWithoutPunc(CellPhone) == StringWithoutPunc(second.CellPhone))
                    return true;
            }
            if (Fax != null && Fax.Length > 0)
            {
                if (StringWithoutPunc(Fax) == StringWithoutPunc(second.Fax))
                    return true;
            }
            if (HomePhone != null && HomePhone.Length > 0)
            {
                if (StringWithoutPunc(HomePhone) == StringWithoutPunc(second.HomePhone))
                    return true;
            }
            if (WorkPhone != null && WorkPhone.Length > 0)
            {
                if (StringWithoutPunc(WorkPhone) == StringWithoutPunc(second.WorkPhone))
                    return true;
            }
            if (Email != null && Email.Length > 0)
            {
                if (Email == second.Email)
                    return true;
            }
            if (Facebook != null && Facebook.Length > 0)
            {
                if (Facebook == second.Facebook)
                    return true;
            }
            if (Twitter != null && Twitter.Length > 0)
            {
                if (Twitter == second.Twitter)
                    return true;
            }

            return false;
        }

        private string StringWithoutPunc(string str)
        {
            if (str == null || str.Length == 0) return string.Empty;
            return str.Replace(" ", "").Replace("-", "").Replace(",", "").Replace("(", "").Replace(")", "")
                .Replace("[", "").Replace("]", "").Replace("_", "").Replace("\\", "").Replace("/", "");
        }
    }
}