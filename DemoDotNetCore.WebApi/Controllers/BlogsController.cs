namespace DemoDotNetCore.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Repositories;
    using System.Collections.Generic;

    [Produces("application/json")]
    [Route("api/Blogs")]
    public class BlogsController : Controller
    {
        private readonly Blogs _blogs;

        public BlogsController(Blogs blogs)
        {
            _blogs = blogs;
        }

        [HttpGet]
        public IReadOnlyList<Blog> Get() => _blogs.All();

        [HttpGet("{id}", Name = "GetBlog")]
        public IActionResult Get(int id) => new ObjectResult(_blogs.ById(id));

        [HttpPost]
        public IActionResult Post([FromBody] Blog blog)
        {
            _blogs.Save(blog);
            return CreatedAtRoute("GetBlog", new { id = blog.BlogId }, blog);
        }
    }
}