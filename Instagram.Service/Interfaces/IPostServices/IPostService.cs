using Instagram.Domain.Commons.Responses;
using Instagram.Domain.Entities.Posts;
using Instagram.Service.DTOs.PostDTOs;
using System.Linq.Expressions;

namespace Instagram.Service.Interfaces.IPostServices
{
    public interface IPostService
    {
        Task<BaseResponse<IEnumerable<PostForViewModel>>> GetAllAsync
            (Expression<Func<Post, bool>> predicate = null, Tuple<int, int> pagenation = null);
        Task<BaseResponse<PostForViewModel>> CreateAsync(PostForCreation post);
        Task<BaseResponse<PostForViewModel>> UpdateAsync(Guid id, PostForCreation post);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Post, bool>> expression);
        Task<BaseResponse<PostForViewModel>> GetAsync(Expression<Func<Post, bool>> expression);
    }
}
