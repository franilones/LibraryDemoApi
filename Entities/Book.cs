﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDemoApi.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
