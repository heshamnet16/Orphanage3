using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrphanageDataModel.ValidationAttributes
{
    public class BailEndDateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var startDateProp = validationContext.ObjectType.GetProperty("StartDate");

            if (startDateProp == null)
                throw new ArgumentException("Property with this name not found");

            var isExpiredProp = validationContext.ObjectType.GetProperty("IsExpired");

            if (isExpiredProp == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime)startDateProp.GetValue(validationContext.ObjectInstance);

            var isExpired = (bool)isExpiredProp.GetValue(validationContext.ObjectInstance);

            var endDate = (DateTime)value;

            if (endDate == null)
                return ValidationResult.Success;

            if (!isExpired && DateTime.Now > endDate)
            {
                return new ValidationResult(Properties.Resources.ErrorBailDateEnded, new List<string>() { validationContext.MemberName });
            }
            if (endDate < comparisonValue)
            {
                return new ValidationResult(Properties.Resources.ErrorBailEndDateBiggerThanStartDate, new List<string>() { validationContext.MemberName });
            }
            return ValidationResult.Success;
        }
    }
}