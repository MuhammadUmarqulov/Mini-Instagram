using Instagram.Domain.Commons;
using Instagram.Domain.Entities.Posts;
using System.ComponentModel.DataAnnotations;

namespace Instagram.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        [MaxLength(48)]
        public string FullName { get; set; }
        [MaxLength(64)]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        [MaxLength(64)]
        public string Username { get; set; }
        [MaxLength(128)]
        public string Password { get; set; }

        public ICollection<UserFollowing> Followings { get; set; }
        public ICollection<UserFollowing> Followers { get; set; }
        public ICollection<SavedPost> SavedPosts { get; set; }
        public ICollection<Post> Posts { get; set; }

    }
}
