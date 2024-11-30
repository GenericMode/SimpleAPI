using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

          public class Song
            {
                    public string Title { get; set; }

                    public string Album { get; set; }

                    public string Lyrics { get; set; }
            }

namespace SimpleAPI.Controllers
{
   //[Route("api/[controller]")]
   [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/values
        [HttpGet]
        [Route("api/values")]
    
        public IActionResult GetAllSongs() //IEnumerable<string> Get()
        {
  
            try
            {
            var absolutePath = Path.GetFullPath(@"songs.json");
            var stringContent = System.IO.File.ReadAllText(absolutePath);
    
            var jsonResult = JsonSerializer.Deserialize<List<Song>>(stringContent);
            var songsOnly = jsonResult.Select(x => x.Title);

            return Ok(songsOnly);
            }
            catch (Exception)
            {
            throw;
            }     
        }

    [HttpGet]
    [Route("{album}/values")]
    public IActionResult GetAlbumSongs([FromRoute]string album)
    {
        try
        {
            if (album is null)
                return BadRequest("Album parameter is mandatory");

            var absolutePath = Path.GetFullPath(@"songs.json");
            var stringContent = System.IO.File.ReadAllText(absolutePath);
            var jsonResult = JsonSerializer.Deserialize<List<Song>>(stringContent);
            var songsOnly = jsonResult?.Where(x => x.Album.ToLower() == album.ToLower()).Select(x => x.Title);

            return Ok(songsOnly);
        }
        catch (Exception)
        {
            throw;
        }
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
        [Route("api/values")]
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