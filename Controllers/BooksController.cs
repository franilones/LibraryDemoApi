using LibraryDemoApi.Context;
using LibraryDemoApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        
        public BooksController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return context.Books.Include(x => x.Author).ToList();
        }

        [HttpGet("{id}", Name = "ObtainBook")]
        public ActionResult<Book> Get(int id)
        {
            var book = context.Books.Include(x => x.Author).FirstOrDefault(x => x.Id == id);

            if(null == book)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtainBook", new { id = book.Id }, book);
        }
    }
}
