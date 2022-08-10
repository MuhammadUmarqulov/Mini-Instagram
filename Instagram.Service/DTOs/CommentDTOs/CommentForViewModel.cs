using Instagram.Service.DTOs.UserDTOs;

namespace Instagram.Service.DTOs.PostDTOs.CommentDTOs
{
    public class CommentForViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public virtual UserForViewModel User { get; set; }
    }
}
