namespace DemoDotNetCore.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Repositories;
    using System.Collections.Generic;

    [Produces("application/json")]
    [Route("api/Posts")]
    public class PostsController : Controller
    {
        private readonly Posts _posts;

        public PostsController(Posts posts)
        {
            _posts = posts;
        }

        [HttpGet]
        public IReadOnlyList<Post> Get() => _posts.All();

        [HttpGet("{id}", Name = "GetPost")]
        public IActionResult Get(int id) => new ObjectResult(_posts.ById(id));

        [HttpPost]
        public IActionResult Post([FromBody] Post post)
        {
            _posts.Save(post);
            return CreatedAtRoute("GetPost", new { id = post.PostId }, post);
        }
    }
}