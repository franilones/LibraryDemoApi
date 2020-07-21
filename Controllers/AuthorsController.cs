using AutoMapper;
using LibraryDemoApi.Context;
using LibraryDemoApi.Entities;
using LibraryDemoApi.helpers;
using LibraryDemoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly ILogger<AuthorsController> logger;
        private readonly IMapper mapper;

        //Dependency injection 
        public AuthorsController(ApplicationDBContext context, ILogger<AuthorsController> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(FilterDemo))]
        public ActionResult<IEnumerable<AuthorDTO>> Get()
        {
            logger.LogInformation("Getting actors");
            var authors = context.Authors.Include(x => x.Books).ToList();
            var authorsDTO = mapper.Map<List<AuthorDTO>>(authors);
            return authorsDTO;
        }

        [HttpGet("{id}", Name = "ObtainAuthor")]
        public async Task<ActionResult<AuthorDTO>> Get(int id)
        {
            var author = await context.Authors.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id == id);

            if(null == author)
            {
                logger.LogWarning($"Actor with ID: {id} not found");
                return NotFound();
            }

            var authorDTO = mapper.Map<AuthorDTO>(author);
            return authorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AuthorCreateDTO authorCreate)
        {
            var author = mapper.Map<Author>(authorCreate);
            context.Authors.Add(author);
            await context.SaveChangesAsync();
            var authorDTO = mapper.Map<AuthorDTO>(author);
            //If everything went well it will call the route ObtainSource that we declared before
            return new CreatedAtRouteResult("ObtainAuthor", new { id = authorDTO.Id}, authorDTO);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Author value)
        {
            if(id != value.Id)
            {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Author> Delete(int id)
        {
            var author = context.Authors.FirstOrDefault(x => x.Id == id);

            if(null == author)
            {
                return NotFound();
            }

            context.Authors.Remove(author);
            context.SaveChanges();
            return author;
        }
    }
}
