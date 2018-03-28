using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrphanageDataModel.ValidationAttributes
{


    public class DateOfDeathValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            var dateOfDeath = (DateTime) value;

            if (dateOfDeath == null)
                return ValidationResult.Success;

            if (DateTime.Now.Year - dateOfDeath.Year > 18)
            {
                return new ValidationResult(Properties.Resources.ErrorOverAge, new List<string>() { validationContext.MemberName });
            }
            if (DateTime.Now.Year - dateOfDeath.Year < 0)
            {
                return new ValidationResult(Properties.Resources.ErrorWrongData, new List<string>() { validationContext.MemberName });
            }
            return ValidationResult.Success;
        }
    }
}
