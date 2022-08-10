using Instagram.Domain.Commons;
using Instagram.Domain.Entities.Users;

namespace Instagram.Domain.Entities.Posts
{
    public class Post : BaseEntity
    {
        public Guid? UserId { get; set; }
        public User User { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }

        public ICollection<SavedPost> SavedPosts { get; set; }
        public ICollection<Attachment> Contents { get; set; }
    }


}
