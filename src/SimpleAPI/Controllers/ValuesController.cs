using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;
//using Newtonsoft.Json;


namespace SimpleAPI.Controllers
{
   //[Route("api/[controller]")]
   [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Song _song;

        public ValuesController(Song song)
        {
            _song = song;
        }
 
        // GET: api/values
        [HttpGet]
        [Route("api/values")]
        public IActionResult GetAllSongs() //IEnumerable<string> Get()
        {
  
            try
            {
                
            var absolutePath = _song.GetFilePath();
            var stringContent = System.IO.File.ReadAllText(absolutePath);
    
            var jsonResult = JsonSerializer.Deserialize<List<Song>>(stringContent);
            var songsOnly = jsonResult.Select(x => x.Title);

            return Ok(songsOnly);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error while fetching songs.");
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

            var absolutePath = _song.GetFilePath();
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
        public ActionResult Get(int id)
        {
             try
        {

            var absolutePath = _song.GetFilePath();
            var stringContent = System.IO.File.ReadAllText(absolutePath);
            var jsonResult = JsonSerializer.Deserialize<List<Song>>(stringContent);
            var song = jsonResult?.ElementAtOrDefault(id); // id is the "index"

            return Ok(new { Title = song.Title });
        }
        catch (Exception)
        {
            throw;
        }
        }

        // POST: api/values // post to json
        [HttpPost]
        [Route("api/values")]
        public IActionResult AddSong([FromBody] Song newSong)
        {

            var absolutePath = _song.GetFilePath();
            var stringContent = System.IO.File.ReadAllText(absolutePath);
            var jsonResult = JsonSerializer.Deserialize<List<Song>>(stringContent);
            // Check if song with the same title already exists
            if (jsonResult.Any(s => s.Title.Equals(newSong.Title, StringComparison.OrdinalIgnoreCase)))
                return Conflict("A song with this title already exists.");

         jsonResult.Add(newSong);
         _song.SaveSongsToFile(jsonResult);

         // Return a 201 Created response with the Location header pointing to the newly created song by its index
        return CreatedAtAction(nameof(Get), new { id = jsonResult.Count - 1 }, newSong); // Use the index (songs.Count - 1)
        }
        

        // PUT: api/values/5
        // Update an existing song by Title
        [HttpPut("api/songs/{title}")]
        //[Route("api/values/{title}")]
       public IActionResult UpdateSong(string title, [FromBody] Song updatedSong)
        {
            var absolutePath = _song.GetFilePath();
            var stringContent = System.IO.File.ReadAllText(absolutePath);
            var jsonResult = JsonSerializer.Deserialize<List<Song>>(stringContent);
            var existingSong = jsonResult.FirstOrDefault(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (existingSong == null)
                return NotFound("Song not found.");

            // Update the song's properties
            existingSong.Title = updatedSong.Title;
            existingSong.Album = updatedSong.Album;
            existingSong.Lyrics = updatedSong.Lyrics;

            _song.SaveSongsToFile(jsonResult);

            return Ok(existingSong);
        }

    // Delete a song by Title
        [HttpDelete("api/songs/{title}")]
        //[Route("api/values/{title}")]
        public IActionResult DeleteSong(string title)
        {
            var absolutePath = _song.GetFilePath();
            var stringContent = System.IO.File.ReadAllText(absolutePath);
            var jsonResult = JsonSerializer.Deserialize<List<Song>>(stringContent);
            var songToDelete = jsonResult.FirstOrDefault(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (songToDelete == null)
                return NotFound("Song not found.");

            jsonResult.Remove(songToDelete);
            _song.SaveSongsToFile(jsonResult);

            return NoContent(); // Successfully deleted
        }
}
}