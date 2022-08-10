using Instagram.Service.DTOs.AttachmentDTOs;

namespace Instagram.Service.Interfaces.IPostServices
{
    public interface IAttachmentService
    {
        Task<IEnumerable<AttachmentForViewModel>> CreateRangeAsync(string path, IEnumerable<AttachmentForCreation> attachment);

        Task<bool> UpdateAsync(Guid id, Guid postId);
    }
}
