using EF_Core.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EF_Core.Validation
{
    internal class IsEmptyLoginRole : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo
       cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();
            if (input == string.Empty)
            {
                return new ValidationResult(false, "Поле должно быть заполнено");
            }

            if (input.Length < 5)
            {
                return new ValidationResult(false, "Минимум 5 символов");
            }

            return ValidationResult.ValidResult;

        }

    }
}
