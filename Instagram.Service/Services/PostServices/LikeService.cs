using Instagram.Data.IRepositories;
using Instagram.Data.Repositories;
using Instagram.Domain.Commons.Responses;
using Instagram.Domain.Entities.Posts;
using Instagram.Service.DTOs.LikeDTOs;
using Instagram.Service.DTOs.UserDTOs;
using Instagram.Service.Extentions;
using Instagram.Service.Interfaces.IPostServices;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Instagram.Service.Services.PostServices
{
#pragma warning disable
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly TypeAdapterConfig config;

        public LikeService()
        {
            unitOfWork = new UnitOfWork();

            config = new TypeAdapterConfig();
            config.NewConfig<Like, LikeForViewModel>()
                    .Map(dest => dest.User, src => src.Adapt<UserForViewModel>());
        }

        public async Task<BaseResponse<bool>> CreateAsync(LikeForCreation like)
        {
            var response = new BaseResponse<bool>();

            if (unitOfWork.Posts.GetAsync(post => post.Id == like.PostId) is null)
            {
                response.Error = new ErrorResponse(message: "Post does not exist", 400);

                return response;
            }

            else if (unitOfWork.Users.GetAsync(user => user.Id == like.UserId) is null)
            {
                response.Error = new ErrorResponse(message: "User does not exist", 400);

                return response;
            }

            var exist = await unitOfWork.Likes.GetAsync
                (l => l.UserId == like.UserId && l.PostId == like.PostId);

            if (exist is not null)
            {
                await unitOfWork.Likes.DeleteAsync(l => l.UserId == like.UserId && l.PostId == like.PostId);

                response.Data = false;
            }

            else
            {
                await unitOfWork.Likes.CreateAsync(like.Adapt<Like>());

                response.Data = true;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<LikeForViewModel>>> GetAllAsync
            (Expression<Func<Like, bool>> expression = null, Tuple<int, int> pagenation = null)
        {
            var response = new BaseResponse<IEnumerable<LikeForViewModel>>();

            response.Data = unitOfWork.Likes.GetAll(expression)
                .GetWithPagenation(pagenation)
                    .Include(p => p.User)
                        .Adapt<IEnumerable<LikeForViewModel>>(config);

            return response;
        }
    }
}
