using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.ViewModel
{
    public class ValidDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime result;
            var isValid = DateTime.TryParseExact(
                Convert.ToString(value),
                "d MMM yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out result);

            return (isValid && result > DateTime.Now);
        }
    }

    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime result;
            var isValid = DateTime.TryParseExact(
                Convert.ToString(value),
                "HH:MM",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out result);

            return (isValid);
        }
    }
}