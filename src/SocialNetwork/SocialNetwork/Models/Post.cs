namespace SocialNetwork.Models
{
    public class Post
    {
        public string UserName { get; set; }
        public string Text { get; set; }

        public Post(string userName, string text)
        {
            UserName = userName;
            Text = text;
        }
    }
}