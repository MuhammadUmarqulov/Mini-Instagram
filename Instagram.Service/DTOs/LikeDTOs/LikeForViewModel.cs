using Instagram.Service.DTOs.UserDTOs;

namespace Instagram.Service.DTOs.LikeDTOs
{
    public class LikeForViewModel
    {
        public Guid Id { get; set; }

        public UserForViewModel User { get; set; }
    }
}
