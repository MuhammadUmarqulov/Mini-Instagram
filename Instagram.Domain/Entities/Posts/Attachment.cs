namespace Instagram.Domain.Entities.Posts
{
    public class Attachment
    {
        public Guid Id { get; set; }

        public string Path { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
