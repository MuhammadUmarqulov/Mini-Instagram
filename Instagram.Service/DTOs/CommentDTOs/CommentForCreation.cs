namespace Instagram.Service.DTOs.PostDTOs.CommentDTOs
{
    public class CommentForCreation
    {
        public string Text { get; set; }

        public Guid? UserId { get; set; }
        public Guid? PostId { get; set; }
    }
}
