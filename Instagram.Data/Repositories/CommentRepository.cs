using Instagram.Data.IRepositories;
using Instagram.Domain.Entities.Posts;

namespace Instagram.Data.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
    }
}
