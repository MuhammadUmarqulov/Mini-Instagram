using Instagram.Data.IRepositories;
using Instagram.Data.Repositories;
using Instagram.Domain.Commons.Responses;
using Instagram.Domain.Entities.Posts;
using Instagram.Service.DTOs.PostDTOs;
using Instagram.Service.DTOs.SavedPostDTOs;
using Instagram.Service.Extentions;
using Instagram.Service.Interfaces.IPostServices;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Instagram.Service.Services.PostServices
{
#pragma warning disable
    public class SavedPostService : ISavedPostService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly TypeAdapterConfig config;

        public SavedPostService()
        {
            unitOfWork = new UnitOfWork();

            config = new TypeAdapterConfig();
            config.NewConfig<SavedPost, SavedPostForViewModel>()
                .Map(dest => dest.Post, src => src.Adapt<PostForViewModel>());
        }

        public async Task<BaseResponse<bool>> CreateAsync(SavedPostForCreation savedPost)
        {
            var response = new BaseResponse<bool>();

            if (unitOfWork.Posts.GetAsync(post => post.Id == savedPost.PostId) is null)
            {
                response.Error = new ErrorResponse(message: "Post does not exist", 400);

                return response;
            }

            else if (unitOfWork.Users.GetAsync(user => user.Id == savedPost.UserId) is null)
            {
                response.Error = new ErrorResponse(message: "User does not exist", 400);

                return response;
            }

            var exist = await unitOfWork.SavedPosts.GetAsync
                (sp => sp.UserId == savedPost.UserId && sp.PostId == savedPost.PostId);

            if (exist is not null)
            {
                await unitOfWork.SavedPosts.DeleteAsync
                    (sp => sp.UserId == savedPost.UserId && sp.PostId == savedPost.PostId);

                response.Error = new ErrorResponse(message: "Post unsaved successfully", 200);
            }

            else
            {
                await unitOfWork.SavedPosts.CreateAsync(savedPost.Adapt<SavedPost>());

                response.Data = true;
            }

            return response;
        }


        public async Task<BaseResponse<IEnumerable<SavedPostForViewModel>>> GetAllAsync
            (Expression<Func<SavedPost, bool>> expression = null, Tuple<int, int> pagenation = null)
        {
            var response = new BaseResponse<IEnumerable<SavedPostForViewModel>>();

            response.Data = unitOfWork.SavedPosts.GetAll(expression)
                            .GetWithPagenation(pagenation)
                                .Include("Post").GetWithPagenation(pagenation)
                                        .Adapt<IEnumerable<SavedPostForViewModel>>(config);

            return response;
        }

    }
}
