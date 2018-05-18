//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using MediaManager.ApiModels;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace MediaManager.Controllers
//{
//    [Produces("application/json")]
//    [Route("api/songs")]
//    public class SongController : Controller
//    {
//        private static List<Song> songs = new List<Song>();

//        //User
//        [HttpGet]
//        [Route("{id}")]
//        public async Task<ActionResult> GetById(int id)
//        {
//            return Ok(songs.Where(s => s.Id == id).FirstOrDefault());
//        }

//        //User
//        [HttpGet]
//        [Route("")]
//        public async Task<ActionResult> Get()
//        {
//            return Ok(songs);
//        }

//        //Admin
//        [HttpPost]
//        [Route("")]
//        public async Task<ActionResult> Create([FromBody] Song song)
//        {
//            if (string.IsNullOrWhiteSpace(song.Name)
//                || string.IsNullOrWhiteSpace(song.Content))
//            {
//                return BadRequest();
//            }

//            songs.Add(song);

//            return Created("Created Song", song);
//        }

//        //Admin
//        [HttpPut]
//        [Route("")]
//        public async Task<ActionResult> UpdateEntity([FromBody] Song song)
//        {
//            ActionResult result = BadRequest();

//            if (song.Id is null
//                || string.IsNullOrWhiteSpace(song.Name)
//                || string.IsNullOrWhiteSpace(song.Content))
//            {
//                return result;
//            }

//            var foundSong = songs.Where(s => s.Id == song.Id).FirstOrDefault();

//            if (!(foundSong is null))
//            {
//                foundSong.Name = song.Name;
//                foundSong.Content = song.Content;

//                result = Ok();
//            }
//            else
//            {
//                result = NotFound();
//            }

//            return result;
//        }

//        //Admin
//        [HttpPatch]
//        [Route("")]
//        public async Task<ActionResult> UpdateProperty([FromBody] Song song)
//        {
//            return await UpdateEntity(song);
//        }

//        //Admin
//        [HttpDelete]
//        [Route("{id}")]
//        public async Task<ActionResult> Delete(int id)
//        {
//            songs.Remove(songs.Where(s => s.Id == id).FirstOrDefault());
//            return Ok();
//        }
//    }
//}