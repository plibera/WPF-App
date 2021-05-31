using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace wpf_piotr_libera.Models.Validation
{
    public class NumericRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = value.ToString();
            if (str == "")
                return ValidationResult.ValidResult;
            double val;
            if (Double.TryParse(str, out val))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "A number is required.");
            }
        }
    }
}