using Instagram.Domain.Commons.Responses;
using Instagram.Domain.Entities.Posts;
using Instagram.Service.DTOs.SavedPostDTOs;
using System.Linq.Expressions;

namespace Instagram.Service.Interfaces.IPostServices
{
    public interface ISavedPostService
    {
        /// <summary>
        /// Add saved post for post given by argument post id and user id 
        /// </summary>
        /// <param name="savedPost"> dto for add saved post </param>
        /// <returns> response with view model </returns>
        Task<BaseResponse<bool>> CreateAsync(SavedPostForCreation savedPost);

        /// <summary>
        /// Get All saved Post by given Expression for Get all saved post in the post
        /// </summary>
        /// <param name="expression"> Func<Like, bool> predicate</param>
        /// <returns> list of the saved posts View models </returns>
        Task<BaseResponse<IEnumerable<SavedPostForViewModel>>> GetAllAsync
            (Expression<Func<SavedPost, bool>> expression = null, Tuple<int, int> pagenation = null);

    }
}
