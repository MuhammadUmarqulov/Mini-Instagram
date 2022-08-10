using Instagram.Domain.Commons.Responses;
using Instagram.Domain.Entities.Users;
using Instagram.Service.DTOs.UserDTOs;
using System.Linq.Expressions;

namespace Instagram.Service.Interfaces.IUserServices
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<UserForViewModel>>> GetAllAsync
            (Expression<Func<User, bool>> predicate = null, Tuple<int, int> pagenation = null);

        Task<BaseResponse<UserForViewModel>> CreateAsync(UserForCreation user);

        Task<BaseResponse<UserForViewModel>> UpdateAsync(Guid id, UserForUpdate user);

        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression);

        Task<BaseResponse<UserForViewModel>> GetAsync(Expression<Func<User, bool>> expression);

        Task<BaseResponse<UserForViewModel>> CheckLoginAsync(UserForLogin login);

        Task<BaseResponse<UserForViewModel>> ChangePasswordAsync(UserForChangePassword user);
    }
}
