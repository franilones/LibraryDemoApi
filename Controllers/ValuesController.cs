using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //GET api/values
        [HttpGet(Name = "ObtainValues")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //GET api/values/5
        [HttpGet("{id}", Name = "ObtainValue")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        //POST api/values
        [HttpPost(Name = "CreateValue")]
        public void Post([FromBody] string value)
        {
        }

        //PUT api/values/5
        [HttpPut("{id}", Name = "UpdateValue")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //DELETE api/values/5
        [HttpDelete("{id}", Name = "DeleteValue")]
        public void Delete(int id)
        {
        }
    }
}
