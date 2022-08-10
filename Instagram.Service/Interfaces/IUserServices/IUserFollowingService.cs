using Instagram.Domain.Commons.Responses;
using Instagram.Domain.Entities.Users;
using Instagram.Service.DTOs.UserFollowingDTOs;
using System.Linq.Expressions;

namespace Instagram.Service.Interfaces.IUserServices
{
    public interface IUserFollowingService
    {
        /// <summary>
        /// Add or remove Following for post given by argument user id 
        /// </summary>
        /// <param name="like"> dto for add follow </param>
        /// <returns> response with view model </returns>
        Task<BaseResponse<bool>> CreateAsync(UserFollowingForCreation follow);


        /// <summary>
        /// Get All follow by given Expression for Get all follows
        /// </summary>
        /// <param name="expression"> Func<UserFollowing, bool> predicate</param>
        /// <returns> list of the Following View models </returns>
        Task<BaseResponse<IEnumerable<UserFollowingForViewModel>>> GetAllAsync
            (Expression<Func<UserFollowing, bool>> expression, Tuple<int, int> pagenation = null);

    }
}
