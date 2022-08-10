using Instagram.Domain.Commons;
using Instagram.Domain.Entities.Users;

namespace Instagram.Domain.Entities.Posts
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }

        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        public Guid? PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
