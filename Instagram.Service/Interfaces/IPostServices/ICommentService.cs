using Instagram.Domain.Commons.Responses;
using Instagram.Domain.Entities.Posts;
using Instagram.Service.DTOs.PostDTOs.CommentDTOs;
using System.Linq.Expressions;

namespace Instagram.Service.Interfaces.IPostServices
{
    public interface ICommentService
    {
        /// <summary>
        /// Add comment for post given by argument post id and user id 
        /// </summary>
        /// <param name="like"> dto for add comment </param>
        /// <returns> response with view model </returns>
        Task<BaseResponse<CommentForViewModel>> CreateAsync(CommentForCreation comment);

        /// <summary>
        /// Get All comments by given Expression for Get all comment in the post
        /// </summary>
        /// <param name="expression"> Func<Like, bool> predicate</param>
        /// <returns> list of the comments View models </returns>
        Task<BaseResponse<IEnumerable<CommentForViewModel>>> GetAllAsync
            (Expression<Func<Comment, bool>> expression = null, Tuple<int, int> pagenation = null);

        /// <summary>
        /// Delete Comment from post by given expression.
        /// </summary>
        /// <param name="expression"> Func<Comment, bool> predicate </param>
        /// <returns> if comment is deleted successfully resturn true else false </returns>
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Comment, bool>> expression);
    }
}
