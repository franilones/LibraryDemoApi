using LibraryDemoApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDemoApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = "GetRoot")]
        public ActionResult<IEnumerable<Link>> Get()
        {
            List<Link> links = new List<Link>();

            links.Add(new Link(Url.Link("GetRoot", new { }), "self", "GET"));
            links.Add(new Link(Url.Link("ObtainAuthors", new { }), "get-authors", "GET"));
            links.Add(new Link(Url.Link("CreateAuthor", new { }), "new-author", "POST"));
            links.Add(new Link(Url.Link("ObtainValues", new { }), "obtain-values", "GET"));
            links.Add(new Link(Url.Link("CreateValue", new { }), "create-value", "POST"));

            return links;
        }
    }
}
