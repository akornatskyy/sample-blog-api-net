namespace Blog.Models
{
    public sealed class AuthInfo
    {
        public string UserId { get; set; }

        public string Password { get; set; }

        public bool IsLocked { get; set; }
    }
}