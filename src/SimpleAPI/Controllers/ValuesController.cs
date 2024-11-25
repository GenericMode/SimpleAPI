using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleAPI.Controllers
{
   //[Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET: api/values
        [HttpGet]
        [Route("api/values")]
    
        public IEnumerable<string> Get()
        {
            return new string[] { "Test value PT2", "Test value FL2" };
        }

        // GET: api/values/5
        [HttpGet]
        [Route("api/values/{id}")]
        public string Get(int id)
        {
            string Value = "Les Jackson";
            return Value;
        }

        // POST: api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/values/5
        [HttpPut]
        [Route("api/values/{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/values/5
        [HttpDelete]
        [Route("api/values/{id}")]
        public void Delete(int id)
        {
        }
    }
}