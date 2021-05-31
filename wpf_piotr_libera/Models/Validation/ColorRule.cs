using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace wpf_piotr_libera.Models.Validation
{
    public class ColorRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = value.ToString();
            if (str == "")
                return ValidationResult.ValidResult;
            foreach (char c in str)
            {
                if (c < 'a' || c > 'z')
                    return new ValidationResult(false, "Color name should only consist of small letters.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
