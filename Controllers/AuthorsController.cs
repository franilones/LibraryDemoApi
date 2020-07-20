using LibraryDemoApi.Context;
using LibraryDemoApi.Entities;
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

        //Dependency injection 
        public AuthorsController(ApplicationDBContext context, ILogger<AuthorsController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get()
        {
            logger.LogInformation("Getting actors");
            return context.Authors.Include(x => x.Books).ToList();
        }

        [HttpGet("{id}", Name = "ObtainResource")]
        public async Task<ActionResult<Author>> Get(int id)
        {
            var author = await context.Authors.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id == id);

            if(null == author)
            {
                logger.LogWarning($"Actor with ID: {id} not found");
                return NotFound();
            }
            return author;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Author author)
        {
            context.Authors.Add(author);
            context.SaveChanges();
            //If everything went well it will call the route ObtainSource that we declared before
            return new CreatedAtRouteResult("ObtainResource", new { id = author.Id}, author);
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
