using ImageSearchWPF.Utils;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace ImageSearchWPF.CustomValidations
{
    /// <summary>
    /// Checks if string is null or empty or only  contains spaces,
    /// if it is so then string is not valid
    /// </summary>
    public class EmptyStringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // checks string is empty  or contains only spaces
            if (String.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, ConstantsUtility.EmptySearchStringErrorMessage);
            }

            return ValidationResult.ValidResult;
        }

    }
}
