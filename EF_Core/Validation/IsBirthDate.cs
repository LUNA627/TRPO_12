using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EF_Core.Validation
{
    internal class IsBirthDate : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string dateStr && !string.IsNullOrWhiteSpace(dateStr))
            {
                if (DateTime.TryParseExact(dateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    if (date > DateTime.Today)
                        return new ValidationResult(false, "Дата рождения не может быть в будущем.");

                    if (date < new DateTime(1900, 1, 1))
                        return new ValidationResult(false, "Дата слишком ранняя.");

                    return ValidationResult.ValidResult;
                }
                return new ValidationResult(false, "Введите дату в формате: dd.MM.yyyy");
            }

            return new ValidationResult(false, "Дата рождения обязательна.");
        }
    }
}
