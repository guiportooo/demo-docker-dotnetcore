namespace DemoDotNetCore.WebApi.Controllers
{
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Microsoft.AspNetCore.Mvc;
    using MimeKit;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Produces("application/json")]
    [Route("api/Comments")]
    public class CommentsController : Controller
    {
        private readonly BloggingContext _bloggingContext;
        public const string MAIL_HOST = "mail";
        public const int MAIL_PORT = 1025;

        public CommentsController(BloggingContext bloggingContext)
        {
            _bloggingContext = bloggingContext;
        }

        [HttpGet]
        public IReadOnlyList<Comment> Get() => _bloggingContext.Comments.ToList();

        [HttpGet("{id}", Name = "GetComment")]
        public IActionResult Get(int id) => new ObjectResult(_bloggingContext.Comments.Find(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Comment Comment, string email = "test@fake.com")
        {
            if (Comment.CommentId == 0)
            {
                _bloggingContext.Add(Comment);
            }

            _bloggingContext.SaveChanges();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("BlogApi", "blogapi@demodocker.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = $"New comment from {Comment.Author}!";

            message.Body = new TextPart("plain")
            {
                Text = Comment.Content
            };

            using (var mailClient = new SmtpClient())
            {
                await mailClient.ConnectAsync(MAIL_HOST, MAIL_PORT, SecureSocketOptions.None);
                await mailClient.SendAsync(message);
                await mailClient.DisconnectAsync(true);
            }

            return CreatedAtRoute("GetComment", new { id = Comment.CommentId }, Comment);
        }
    }
}