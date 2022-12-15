using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/<PostController>
        [HttpGet]
        [Authorize(Roles = "admin, author, reader")]
        public IActionResult Get()
        {
            var posts = _postService.GetAll();
            return posts == null ? NotFound() : Ok(posts);
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, author, reader")]
        public IActionResult Get(int id)
        {
            var post = _postService.Get(id);
            return post == null ? NotFound() : Ok(post);
        }

        // POST api/<PostController>
        [HttpPost]
        [Authorize(Roles = "author")]
        public IActionResult Post([FromBody] PostModel post)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var idpost = _postService.Create(post);
            return Ok(idpost);
        }

        // PUT api/<PostController>
        [HttpPut]
        [Authorize(Roles = "author")]
        public IActionResult Put([FromBody] PostModel post)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var postFromDb = _postService.Get(post.Id);
            if (postFromDb == null || postFromDb.UserId != post.UserId) return BadRequest("В запросе не верно указан Id");
            _postService.Update(post);
            return NoContent();
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            _postService.Delete(id);
            return NoContent();
        }

        // GET api/<PostController>/SelectedPosts/5
        [HttpGet("SelectedPosts/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetPostsByTagId([FromRoute] int id)
        {
            var posts = _postService.GetPostsByTagId(id);
            return posts == null ? NotFound() : Ok(posts);
        }

        // GET api/<PostController>/ChartByDate
        [HttpGet("CharByDate")]
        [Authorize(Roles = "admin")]
        public IActionResult GetChartByDate()
        {
            var chart = _postService.GetChartByDate();
            return chart == null ? NotFound() : Ok(chart);
        }

        // GET api/<PostController>/ChartByUser
        [HttpGet("CharByUser")]
        [Authorize(Roles = "admin")]
        public IActionResult GetChartByUser()
        {
            var chart = _postService.GetChartByUser();
            return chart == null ? NotFound() : Ok(chart);
        }

        // GET api/<PostController>/ChartByTag
        [HttpGet("CharByTag")]
        [Authorize(Roles = "admin")]
        public IActionResult GetChartByTag()
        {
            var chart = _postService.GetChartByTag();
            return chart == null ? NotFound() : Ok(chart);
        }
    }
}
