using Instagram.Domain.Commons.Responses;
using Instagram.Domain.Entities.Posts;
using Instagram.Service.DTOs.LikeDTOs;
using System.Linq.Expressions;

namespace Instagram.Service.Interfaces.IPostServices
{
    public interface ILikeService
    {

        /// <summary>
        /// Add like for post given by argument post id and user id 
        /// </summary>
        /// <param name="like"> dto for add like </param>
        /// <returns> response with view model </returns>
        Task<BaseResponse<bool>> CreateAsync(LikeForCreation like);


        /// <summary>
        /// Get all like by given Expression for Get all like in the post
        /// </summary>
        /// <param name="expression"> Func<Like, bool> predicate</param>
        /// <returns> list of the Like View models </returns>
        Task<BaseResponse<IEnumerable<LikeForViewModel>>> GetAllAsync
            (Expression<Func<Like, bool>> expression = null, Tuple<int, int> pagenation = null);
    }
}
