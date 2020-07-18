using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDemoApi.helpers
{
    public class StartWithCapitalAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //We return a success because we already have a Required verification
            if(null == value || string.IsNullOrEmpty(value.ToString())){
                return new ValidationResult("First letter has to be capital");
            }

            var letter = value.ToString()[0].ToString();

            if(letter != letter.ToUpper())
            {
                return new ValidationResult("First letter has to be capital");
            }

            return ValidationResult.Success;
        }
    }
}
