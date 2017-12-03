namespace DemoDotNetCore.WebApi.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class Posts
    {
        private readonly BloggingContext _bloggingContext;

        public Posts(BloggingContext bloggingContext)
        {
            _bloggingContext = bloggingContext;
        }

        public IReadOnlyList<Post> All() => _bloggingContext.Posts.ToList();

        public Post ById(int id) => _bloggingContext.Posts.Find(id);

        public void Save(Post post)
        {
            if (post.PostId == 0)
            {
                _bloggingContext.Add(post);
            }

            _bloggingContext.SaveChanges();
        }
    }
}
