using LibraryDemoApi.helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDemoApi.Entities
{
    public class Author
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
    }
}
