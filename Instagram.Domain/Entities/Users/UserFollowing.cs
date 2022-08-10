namespace Instagram.Domain.Entities.Users
{
    public class UserFollowing
    {

        public Guid Id { get; set; }

        public Guid? FollowFrom { get; set; }
        public User FollowingFrom { get; set; }

        public Guid? FollowTo { get; set; }
        public User FollowingTo { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
