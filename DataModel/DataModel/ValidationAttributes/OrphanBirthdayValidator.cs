using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrphanageDataModel.ValidationAttributes
{


    public class OrphanBirthdayValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var birthday = (DateTime) value;

            if (birthday == null)
                return ValidationResult.Success;

            if (DateTime.Now.Year - birthday.Year > 18)
            {
                return new ValidationResult(Properties.Resources.ErrorOverAge, new List<string>() { validationContext.MemberName });
            }
            if (DateTime.Now.Year - birthday.Year < 0)
            {
                return new ValidationResult(Properties.Resources.ErrorOverAge, new List<string>() { validationContext.MemberName });
            }
            return ValidationResult.Success;
        }
    }
}
