using LibraryDemoApi.helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDemoApi.Entities
{
    public class Author : IValidatableObject
    {
        public int Id { get; set; }
        [StartWithCapital]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "Name has to contain at least {1} character")]
        [Required]
        public string Name { get; set; }
        [Range(16, 110)]
        public int Edad { get; set; }
        [CreditCard]
        public string CreditCard { get; set; }
        [Url]
        public string Url { get; set; }
        public List<Book> Books { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Here we can validate the entire model
            if (!string.IsNullOrEmpty(Name))
            {
                var letter = Name[0].ToString();

                if(letter != letter.ToUpper())
                {
                    yield return new ValidationResult("First letter must be capital");
                }
            }

            if(Edad < 18 || Edad > 110)
            {
                yield return new ValidationResult("Age is not valid");
            }
        }
    }
}
