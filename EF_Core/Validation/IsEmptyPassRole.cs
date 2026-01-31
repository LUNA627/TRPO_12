using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EF_Core.Validation
{
    internal class IsEmptyPassRole : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo
     cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();
            if (input == string.Empty)
            {
                return new ValidationResult(false, "Поле должно быть заполнено");
            }

            if (input.Length < 8)
            {
                return new ValidationResult(false, "Минимум 8 символов");
            }

            var inputUpper = input.Any(char.IsUpper);
            var inputLower = input.Any(char.IsLower);
            var inputDigit = input.Any(char.IsDigit);
            var symbols = input.Any(c => @"!@#$%^&*_+".Contains(c));

            if (!inputUpper)
            {
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну Заглавную букву");
            }
            if (!inputLower)
            {
                return new ValidationResult(false, "Пароль должен содержать хоты бы одну Строчную букву");
            }
            if (!inputDigit)
            {
                return new ValidationResult(false, "Пароль должен содержать хоты бы одну Цифру");
            }
            if (!symbols)
            {
                return new ValidationResult(false, "Пароль должен содержать хоты бы один спецсимвол");
            }

            return ValidationResult.ValidResult;

        }

    }
}
