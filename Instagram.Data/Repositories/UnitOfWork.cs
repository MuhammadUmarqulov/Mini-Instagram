using Instagram.Data.Contexts;
using Instagram.Data.IRepositories;

namespace Instagram.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserFollowingRepository UsersFollow { get; }

        public IUserRepository Users { get; }

        public ILikeRepository Likes { get; }

        public ICommentRepository Comments { get; }

        public IPostRepository Posts { get; }

        public ISavedPostRepository SavedPosts { get; }

        public InstagramDbContext dbContext { get; set; }

        public IAttachmentRepository Attachments { get; }

        public UnitOfWork()
        {
            UsersFollow = new UserFollowingRepository();
            Comments = new CommentRepository();
            Users = new UserRepository();
            Likes = new LikeRepository();
            Posts = new PostRepository();
            SavedPosts = new SavedPostRepository();
            Attachments = new AttachmentRepository();

            dbContext = new InstagramDbContext();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
