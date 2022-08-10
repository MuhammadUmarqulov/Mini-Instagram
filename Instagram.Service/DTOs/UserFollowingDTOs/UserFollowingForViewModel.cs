using Instagram.Service.DTOs.UserDTOs;

namespace Instagram.Service.DTOs.UserFollowingDTOs
{
    public class UserFollowingForViewModel
    {
        public Guid Id { get; set; }

        public UserForViewModel FollowingTo { get; set; }
    }
}
