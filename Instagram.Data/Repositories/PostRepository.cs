using Instagram.Data.IRepositories;
using Instagram.Domain.Entities.Posts;

namespace Instagram.Data.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
    }
}
