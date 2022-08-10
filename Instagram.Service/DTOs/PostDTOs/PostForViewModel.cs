using Instagram.Domain.Entities.Posts;
using Instagram.Service.DTOs.LikeDTOs;
using Instagram.Service.DTOs.PostDTOs.CommentDTOs;
using Instagram.Service.DTOs.UserDTOs;

namespace Instagram.Service.DTOs.PostDTOs
{
    public class PostForViewModel
    {
        public Guid Id { get; set; }
        public UserForViewModel User { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Hashtags { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
        public string MarkerUsersUsernames { get; set; } = string.Empty;

        public IEnumerable<CommentForViewModel> Comments { get; set; }
        public IEnumerable<LikeForViewModel> Likes { get; set; }
        public IEnumerable<Attachment> Contents { get; set; }

    }
}
