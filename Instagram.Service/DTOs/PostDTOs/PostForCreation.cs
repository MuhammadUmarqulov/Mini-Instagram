using Instagram.Service.DTOs.AttachmentDTOs;

namespace Instagram.Service.DTOs.PostDTOs
{
    public class PostForCreation
    {
        public Guid? UserId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public IEnumerable<AttachmentForCreation> Contents { get; set; }

    }
}
