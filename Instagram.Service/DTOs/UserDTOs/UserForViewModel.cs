using Instagram.Domain.Entities.Posts;
using System.ComponentModel.DataAnnotations;

namespace Instagram.Service.DTOs.UserDTOs
{
    public class UserForViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Username { get; set; }

        public ICollection<Post> Posts { get; set; }

    }
}
