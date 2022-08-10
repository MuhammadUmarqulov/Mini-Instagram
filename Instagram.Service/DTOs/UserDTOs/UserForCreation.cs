namespace Instagram.Service.DTOs.UserDTOs
{
    public class UserForCreation
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
