using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrphanageDataModel.ValidationAttributes
{
    public class AccountAlwaysPositiv : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var canBeNigativProp = validationContext.ObjectType.GetProperty("CanNotBeNegative");

            if (canBeNigativProp == null)
                throw new ArgumentException("Property with this name not found");

            var canBeNegativValue = (bool)canBeNigativProp.GetValue(validationContext.ObjectInstance);

            if (!canBeNegativValue)
            {
                if ((decimal)value < 0)
                    return new ValidationResult(Properties.Resources.ErrorAccountCannotBeNegativ, new List<string>() { validationContext.MemberName });
            }
            return ValidationResult.Success;
        }
    }
}