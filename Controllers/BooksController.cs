using AutoMapper;
using LibraryDemoApi.Context;
using LibraryDemoApi.Entities;
using LibraryDemoApi.Models;
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
        private readonly IMapper mapper;

        public BooksController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> Get()
        {
            var books = context.Books.Include(x => x.Author).ToList();
            var booksDTO = mapper.Map<List<BookDTO>>(books);

            return booksDTO;
        }

        [HttpGet("{id}", Name = "ObtainBook")]
        public ActionResult<BookDTO> Get(int id)
        {
            var book = context.Books.Include(x => x.Author).FirstOrDefault(x => x.Id == id);

            if(null == book)
            {
                return NotFound();
            }

            var bookDTO = mapper.Map<BookDTO>(book);
            return bookDTO;
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
