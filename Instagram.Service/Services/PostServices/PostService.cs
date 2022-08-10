using Instagram.Data.IRepositories;
using Instagram.Data.Repositories;
using Instagram.Domain.Commons.Responses;
using Instagram.Domain.Constants;
using Instagram.Domain.Entities.Posts;
using Instagram.Service.DTOs.LikeDTOs;
using Instagram.Service.DTOs.PostDTOs;
using Instagram.Service.DTOs.PostDTOs.CommentDTOs;
using Instagram.Service.Extentions;
using Instagram.Service.Interfaces.IPostServices;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Instagram.Service.Services.PostServices
{
#pragma warning disable
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAttachmentService _attachmentService;

        private readonly TypeAdapterConfig config;

        public PostService()
        {
            _postRepository = new PostRepository();
            _attachmentService = new AttachmentService();

            config = new TypeAdapterConfig();
        }

        public async Task<BaseResponse<PostForViewModel>> CreateAsync(PostForCreation post)
        {
            var response = new BaseResponse<PostForViewModel>();



            if (post.Description.Split('#').Length > 30)
            {
                response.Error = new ErrorResponse("Maximum 30 hashtags allowed", 400);

                return response;
            }

            var rawFiles = await _attachmentService.CreateRangeAsync
                (MediaData.ROOT_FOLDER_PATH, post.Contents);

            if (rawFiles is null)
            {
                response.Error = new ErrorResponse("Content Adding error", 400);

                return response;
            }

            var createdPost = await _postRepository.CreateAsync(post.Adapt<Post>());

            response.Data = createdPost.Adapt<PostForViewModel>();

            foreach (var rawfile in rawFiles)
                _attachmentService.UpdateAsync(rawfile.Id, response.Data.Id);

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Post, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            if (!await _postRepository.DeleteAsync(expression))
            {
                response.Error = new("Post DontFound", 404);

                return response;
            }

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<PostForViewModel>>> GetAllAsync
            (Expression<Func<Post, bool>> predicate = null, Tuple<int, int> pagenation = null)
        {
            var response = new BaseResponse<IEnumerable<PostForViewModel>>();

            response.Data = _postRepository.GetAll(predicate)
                            .GetWithPagenation(pagenation)
                                .Adapt<IEnumerable<PostForViewModel>>();

            return response;
        }

        public async Task<BaseResponse<PostForViewModel>> GetAsync(Expression<Func<Post, bool>> expression)
        {
            var response = new BaseResponse<PostForViewModel>();

            var exists = _postRepository.GetAll(expression)
                .Include(post => post.Comments)
                    .Include(post => post.Likes)
                        .Include(post => post.Contents)
                            .FirstOrDefault();

            if (exists is null)
            {
                response.Error = new ErrorResponse("Post is not found", 404);

                return response;
            }


            config.NewConfig<Post, PostForViewModel>()
                .Map(dest => dest.Comments, src => src.Adapt<IEnumerable<CommentForViewModel>>())
                    .Map(dest => dest.Likes, src => src.Adapt<IEnumerable<LikeForViewModel>>());

            response.Data = exists.Adapt<PostForViewModel>(config);

            return response;
        }

        public async Task<BaseResponse<PostForViewModel>> UpdateAsync(Guid id, PostForCreation post)
        {
            var response = new BaseResponse<PostForViewModel>();

            var exist = await _postRepository.GetAsync(p => p.Id == id);

            if (exist is null)
            {
                response.Error = new ErrorResponse("Post not Found!!!", 404);

                return response;
            }

            var result = _postRepository.Update(post.Adapt(exist));

            response.Data = response.Adapt<PostForViewModel>();

            return response;
        }
    }
}
