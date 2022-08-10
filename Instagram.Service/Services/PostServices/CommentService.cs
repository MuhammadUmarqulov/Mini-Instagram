using Instagram.Data.IRepositories;
using Instagram.Data.Repositories;
using Instagram.Domain.Commons.Responses;
using Instagram.Domain.Entities.Posts;
using Instagram.Service.DTOs.PostDTOs.CommentDTOs;
using Instagram.Service.DTOs.UserDTOs;
using Instagram.Service.Extentions;
using Instagram.Service.Interfaces.IPostServices;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Instagram.Service.Services.PostServices
{
#pragma warning disable
    public class CommentService : ICommentService
    {
        private readonly TypeAdapterConfig config;

        private readonly IUnitOfWork unitOfWork;

        public CommentService()
        {
            unitOfWork = new UnitOfWork();

            config = new TypeAdapterConfig();
            config.NewConfig<Comment, CommentForViewModel>()
                    .Map(dest => dest.User, src => src.Adapt<UserForViewModel>());
        }

        public async Task<BaseResponse<CommentForViewModel>> CreateAsync(CommentForCreation comment)
        {
            var response = new BaseResponse<CommentForViewModel>();

            if (unitOfWork.Posts.GetAsync(post => post.Id == comment.PostId) is null)
            {
                response.Error = new ErrorResponse(message: "Post does not exist", 400);

                return response;
            }

            else if (unitOfWork.Users.GetAsync(user => user.Id == comment.UserId) is null)
            {
                response.Error = new ErrorResponse(message: "User does not exist", 400);

                return response;
            }


            var newComment = await unitOfWork.Comments.CreateAsync(comment.Adapt<Comment>());

            response.Data = newComment.Adapt<CommentForViewModel>(config);

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Comment, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            response.Data = await unitOfWork.Comments.DeleteAsync(expression);

            return response;
        }

        public async Task<BaseResponse<IEnumerable<CommentForViewModel>>> GetAllAsync
            (Expression<Func<Comment, bool>> expression = null, Tuple<int, int> pagenation = null)
        {
            var response = new BaseResponse<IEnumerable<CommentForViewModel>>();

            response.Data = unitOfWork.Comments.GetAll(expression)
                 .GetWithPagenation(pagenation)
                    .Include(comment => comment.User)
                        .Adapt<IEnumerable<CommentForViewModel>>(config);

            return response;
        }
    }
}
