﻿using AutoMapper;
using LibraryDemoApi.Context;
using LibraryDemoApi.Entities;
using LibraryDemoApi.helpers;
using LibraryDemoApi.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<ActionResult> Put(int id, [FromBody] AuthorCreateDTO authorDTO)
        {
            var author = mapper.Map<Author>(authorDTO);
            author.Id = id;

            context.Entry(author).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<AuthorCreateDTO> patchDocument)
        {
            if(null == patchDocument)
            {
                return BadRequest();
            }

            var authorDB = await context.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if(null == authorDB)
            {
                return NotFound();
            }

            var authorDTO = mapper.Map<AuthorCreateDTO>(authorDB);
            patchDocument.ApplyTo(authorDTO, ModelState);

            mapper.Map(authorDTO, authorDB);

            var isValid = TryValidateModel(authorDB);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Author>> Delete(int id)
        {
            var authorId = await context.Authors.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

            if(default(int) == authorId)
            {
                return NotFound();
            }

            context.Remove(new Author { Id = authorId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
