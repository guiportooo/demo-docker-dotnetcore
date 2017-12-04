namespace DemoDotNetCore.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    [Produces("application/json")]
    [Route("api/Blogs")]
    public class BlogsController : Controller
    {
        private readonly BloggingContext _bloggingContext;

        public BlogsController(BloggingContext bloggingContext)
        {
            _bloggingContext = bloggingContext;
        }

        [HttpGet]
        public IReadOnlyList<Blog> Get() => _bloggingContext.Blogs.ToList();

        [HttpGet("{id}", Name = "GetBlog")]
        public IActionResult Get(int id) => new ObjectResult(_bloggingContext.Blogs.Find(id));

        [HttpPost]
        public IActionResult Post([FromBody] Blog blog)
        {
            if (blog.BlogId == 0)
            {
                _bloggingContext.Add(blog);
            }

            _bloggingContext.SaveChanges();
            return CreatedAtRoute("GetBlog", new { id = blog.BlogId }, blog);
        }
    }
}