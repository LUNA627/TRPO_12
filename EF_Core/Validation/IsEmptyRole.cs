using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EF_Core.Validation
{
    internal class IsEmptyRole : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo
    cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();
            if (input == string.Empty)
            {
                return new ValidationResult(false, "Поле должно быть заполнено");
            }

            return ValidationResult.ValidResult;

        }
    }
}
