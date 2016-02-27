using SocialNetwork.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace SocialNetwork.Controllers
{
    public class PostsController : ApiController
    {
        public static List<Post> Posts = new List<Post>() {
            new Post("Obi-Wan Kenobi","These are not the droids you're looking for"),
            new Post("Darth Vader","I find your lack of faith disturbing")
            };

        [HttpGet]
        public List<Post> GetPosts()
        {
            return Posts;
        }

        [HttpPost]
        public void AddPost(Post post)
        {
            Posts.Add(post);
        }
    }
}
