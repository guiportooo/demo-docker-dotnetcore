using DemoDotNetCore.WebApi.Models;
namespace DemoDotNetCore.WebApi.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    public class Blogs
    {
        private readonly BloggingContext _bloggingContext;

        public Blogs(BloggingContext bloggingContext)
        {
            _bloggingContext = bloggingContext;
        }

        public IReadOnlyList<Blog> All() => _bloggingContext.Blogs.ToList();

        public Blog ById(int id) => _bloggingContext.Blogs.Find(id);

        public void Save(Blog blog)
        {
            if (blog.BlogId == 0)
            {
                _bloggingContext.Add(blog);
            }

            _bloggingContext.SaveChanges();
        }
    }
}
