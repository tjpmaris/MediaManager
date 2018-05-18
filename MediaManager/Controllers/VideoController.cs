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
//    [Route("api/videos")]
//    public class VideoController : Controller
//    {
//        private static List<Video> videos = new List<Video>();

//        //User
//        [HttpGet]
//        [Route("{id}")]
//        public async Task<ActionResult> GetById(int id)
//        {
//            return Ok(videos.Where(s => s.Id == id).FirstOrDefault());
//        }

//        //User
//        [HttpGet]
//        [Route("")]
//        public async Task<ActionResult> Get()
//        {
//            return Ok(videos);
//        }

//        //Admin
//        [HttpPost]
//        [Route("")]
//        public async Task<ActionResult> Create([FromBody] Video video)
//        {
//            if (string.IsNullOrWhiteSpace(video.Name)
//                || string.IsNullOrWhiteSpace(video.Content))
//            {
//                return BadRequest();
//            }

//            video.Id = 1 + (videos.LastOrDefault()?.Id ?? 0);

//            videos.Add(video);

//            return Created("Created Video", video);
//        }

//        //Admin
//        [HttpPut]
//        [Route("")]
//        public async Task<ActionResult> UpdateEntity([FromBody] Video video)
//        {
//            ActionResult result = BadRequest();

//            if (video.Id is null
//                || string.IsNullOrWhiteSpace(video.Name)
//                || string.IsNullOrWhiteSpace(video.Content))
//            {
//                return result;
//            }

//            var foundVideo = videos.Where(s => s.Id == video.Id).FirstOrDefault();

//            if (!(foundVideo is null))
//            {
//                foundVideo.Name = video.Name;
//                foundVideo.Content = video.Content;

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
//        public async Task<ActionResult> UpdateProperty([FromBody] Video video)
//        {
//            return await UpdateEntity(video);
//        }

//        //Admin
//        [HttpDelete]
//        [Route("{id}")]
//        public async Task<ActionResult> Delete(int id)
//        {
//            videos.Remove(videos.Where(s => s.Id == id).FirstOrDefault());
//            return Ok();
//        }
//    }
//}