using Instagram.Data.IRepositories;
using Instagram.Domain.Entities.Users;

namespace Instagram.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
    }
}
