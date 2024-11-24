using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;

namespace SimpleAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET: api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "Test value PT2", "Test value FL2" };
        }

        // GET: api/values/5
        public string Get(int id)
        {
            string Value = "Les Jackson";
            return Value;
        }

        // POST: api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/values/5
        public void Delete(int id)
        {
        }
    }
}