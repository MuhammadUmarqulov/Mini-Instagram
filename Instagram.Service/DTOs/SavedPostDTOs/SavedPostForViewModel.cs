using Instagram.Service.DTOs.PostDTOs;

namespace Instagram.Service.DTOs.SavedPostDTOs
{
    public class SavedPostForViewModel
    {
        public Guid Id { get; set; }
        public PostForViewModel Post { get; set; }
    }
}
