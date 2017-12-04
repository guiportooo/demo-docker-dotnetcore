namespace DemoDotNetCore.WebApi.Models
{
    using System.Collections.Generic;

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
