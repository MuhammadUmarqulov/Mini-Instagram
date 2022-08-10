using Instagram.Data.IRepositories;
using Instagram.Domain.Entities.Users;

namespace Instagram.Data.Repositories
{
    public class UserFollowingRepository : GenericRepository<UserFollowing>, IUserFollowingRepository
    {
    }
}
