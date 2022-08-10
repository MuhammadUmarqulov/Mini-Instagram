using Microsoft.AspNetCore.Http;

namespace Instagram.Service.DTOs.AttachmentDTOs
{

    // post id must be given at controller fuction argument
    public class AttachmentForCreation
    {
        public IFormFile File { get; set; }
    }
}
