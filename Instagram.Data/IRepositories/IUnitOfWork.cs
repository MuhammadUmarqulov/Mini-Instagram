using Instagram.Data.Contexts;

namespace Instagram.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserFollowingRepository UsersFollow { get; }
        public IUserRepository Users { get; }
        public ILikeRepository Likes { get; }
        public ICommentRepository Comments { get; }
        public IPostRepository Posts { get; }
        public ISavedPostRepository SavedPosts { get; }
        public IAttachmentRepository Attachments { get; }
        public InstagramDbContext dbContext { get; set; }
    }
}
