namespace Instagram.Service.DTOs.UserDTOs
{
    public class UserForChangePassword
    {
        public string UsernameOrEmail { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
