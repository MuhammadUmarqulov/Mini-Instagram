using Instagram.Data.IRepositories;
using Instagram.Data.Repositories;
using Instagram.Service.DTOs.AttachmentDTOs;
using Instagram.Service.Interfaces.IPostServices;

namespace Instagram.Service.Services.PostServices
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentService()
        {
            _attachmentRepository = new AttachmentRepository();
        }

        public async Task<IEnumerable<AttachmentForViewModel>> CreateRangeAsync
            (string path, IEnumerable<AttachmentForCreation> attachments)
        {
            ICollection<AttachmentForViewModel> results = new List<AttachmentForViewModel>();

            foreach (var attachment in attachments)
            {
                if (attachment.File is null || attachment.File.Length == 0)
                    return null;


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filePath = Path.Combine
                    (path, $"{Guid.NewGuid():N}.{Path.GetExtension(attachment.File.FileName)}");


                using var fileStream = new FileStream(filePath, FileMode.Create);
                await attachment.File.CopyToAsync(fileStream).ConfigureAwait(false);

                results.Add(new AttachmentForViewModel
                {
                    Path = filePath,
                });

            }

            return results;



        }

        public async Task<bool> UpdateAsync(Guid id, Guid postId)
        {
            var existingAttachment = await _attachmentRepository.GetAsync(a => a.Id == id);

            if (existingAttachment is null)
                return false;

            existingAttachment.PostId = postId;

            _attachmentRepository.Update(existingAttachment);

            return true;
        }
    }
}
