namespace DemoDotNetCore.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    [Produces("application/json")]
    [Route("api/Posts")]
    public class PostsController : Controller
    {
        private readonly BloggingContext _bloggingContext;

        public PostsController(BloggingContext bloggingContext)
        {
            _bloggingContext = bloggingContext;
        }

        [HttpGet]
        public IReadOnlyList<Post> Get() => _bloggingContext.Posts.ToList();

        [HttpGet("{id}", Name = "GetPost")]
        public IActionResult Get(int id) => new ObjectResult(_bloggingContext.Posts.Find(id));

        [HttpPost]
        public IActionResult Post([FromBody] Post post)
        {
            if (post.PostId == 0)
            {
                _bloggingContext.Add(post);
            }

            _bloggingContext.SaveChanges();
            return CreatedAtRoute("GetPost", new { id = post.PostId }, post);
        }
    }
}