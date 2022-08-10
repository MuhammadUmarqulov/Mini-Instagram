using Instagram.Data.IRepositories;
using Instagram.Data.Repositories;
using Instagram.Domain.Commons.Responses;
using Instagram.Domain.Entities.Users;
using Instagram.Service.DTOs.UserDTOs;
using Instagram.Service.Extentions;
using Instagram.Service.Interfaces.IUserServices;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Instagram.Service.Services.UserServices
{
#pragma warning disable
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService()
        {
            unitOfWork = new UnitOfWork();
        }

        public async Task<BaseResponse<UserForViewModel>> ChangePasswordAsync(UserForChangePassword changer)
        {
            var response = new BaseResponse<UserForViewModel>();

            var user = await unitOfWork.Users.GetAsync
                (u => u.Username == changer.UsernameOrEmail || u.Email == changer.UsernameOrEmail);

            if (user is null)
            {
                response.Error = new ErrorResponse("User not found", 404);

                return response;
            }

            if (user.Password != changer.OldPassword.GetHash())
            {
                response.Error = new ErrorResponse("Password is incorrect", 400);

                return response;
            }

            if (changer.NewPassword != changer.ConfirmPassword)
            {
                response.Error = new ErrorResponse("Passwords do not match", 400);

                return response;
            }

            user.Password = changer.NewPassword.GetHash();
            user.UpdatedAt = DateTime.UtcNow;

            response.Data = unitOfWork.Users.Update(user).Adapt<UserForViewModel>();

            return response;

        }

        public async Task<BaseResponse<UserForViewModel>> CheckLoginAsync(UserForLogin login)
        {
            var response = new BaseResponse<UserForViewModel>();

            var user = await unitOfWork.Users.GetAsync
                (user => user.Username == login.UsernameOrEmail || user.Email == login.UsernameOrEmail);

            if (user is null)
            {
                response.Error = new ErrorResponse("User not found", 404);

                return response;
            }

            if (user.Password != login.Password.GetHash())
            {
                response.Error = new ErrorResponse("Password is incorrect", 400);

                return response;
            }

            response.Data = user.Adapt<UserForViewModel>();

            return response;
        }

        public async Task<BaseResponse<UserForViewModel>> CreateAsync(UserForCreation user)
        {
            var response = new BaseResponse<UserForViewModel>();

            var exist = await unitOfWork.Users.GetAsync
                (u => u.Email == user.Email && u.Username == user.Username);

            if (exist is not null)
            {
                response.Error = new("User already exists", 400);

                return response;
            }

            var newUser = user.Adapt<User>();
            newUser.Password = user.Password.GetHash();

            var result = await unitOfWork.Users.CreateAsync(newUser);

            response.Data = result.Adapt<UserForViewModel>();

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var exist = unitOfWork.Users.GetAll(expression)
                .Include(user => user.Posts).ThenInclude(post => post.Comments)
                    .Include(user => user.Posts).ThenInclude(post => post.Likes)
                        .Include(user => user.Posts).ThenInclude(post => post.SavedPosts)
                            .Include(user => user.Followers).Include(user => user.Followings)
                                .Include(user => user.SavedPosts).FirstOrDefault();

            foreach (var post in exist.Posts)
            {
                if (!await unitOfWork.Likes.DeleteRangeAsync(post.Likes))
                {
                    response.Error = new ErrorResponse("User posts Likes deleting fail", 400);
                    response.Data = false;

                    return response;
                }
                if (!await unitOfWork.Comments.DeleteRangeAsync(post.Comments))
                {
                    response.Error = new ErrorResponse("User posts Comments deleting fail", 400);
                    response.Data = false;

                    return response;
                }
                if (!await unitOfWork.SavedPosts.DeleteRangeAsync(post.SavedPosts))
                {
                    response.Error = new ErrorResponse("User posts Saved Posts deleting fails", 400);
                    response.Data = false;

                    return response;
                }
            }

            if (!await unitOfWork.Posts.DeleteRangeAsync(exist.Posts))
            {
                response.Error = new ErrorResponse("Post deleting fail", 400);
                response.Data = false;

                return response;
            }

            if (!await unitOfWork.UsersFollow.DeleteRangeAsync(exist.Followings))
            {
                response.Error = new ErrorResponse("Following deleting fail", 400);
                response.Data = false;

                return response;

            }

            if (!await unitOfWork.UsersFollow.DeleteRangeAsync(exist.Followers))
            {
                response.Error = new ErrorResponse("Follower deleting fail", 400);
                response.Data = false;

                return response;

            }

            if (!await unitOfWork.SavedPosts.DeleteRangeAsync(exist.SavedPosts))
            {
                response.Error = new ErrorResponse("Saved Post Deleting fail", 400);
                response.Data = false;

                return response;
            }

            response.Data = await unitOfWork.Users.DeleteAsync(expression);

            return response;
        }

        public async Task<BaseResponse<IEnumerable<UserForViewModel>>> GetAllAsync
            (Expression<Func<User, bool>> predicate = null, Tuple<int, int> pagenation = null)
        {
            var response = new BaseResponse<IEnumerable<UserForViewModel>>();

            response.Data = unitOfWork.Users.GetAll(predicate)
                    .GetWithPagenation(pagenation)
                        .Adapt<IEnumerable<UserForViewModel>>();

            return response;
        }

        public async Task<BaseResponse<UserForViewModel>> GetAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<UserForViewModel>();

            var exist = unitOfWork.Users.GetAll(expression);

            if (exist.FirstOrDefault() is null)
            {
                response.Error = new ErrorResponse("User not found", 404);

                return response;
            }

            response.Data = exist
                .Include(user => user.Posts)
                    .FirstOrDefault()
                        .Adapt<UserForViewModel>();

            return response;
        }

        public async Task<BaseResponse<UserForViewModel>> UpdateAsync(Guid id, UserForUpdate user)
        {
            var response = new BaseResponse<UserForViewModel>();

            var exist = await unitOfWork.Users.GetAsync(u => u.Id == id);

            if (exist is null)
            {
                response.Error = new ErrorResponse("User not found", 404);

                return response;
            }

            exist = user.Adapt(exist);
            exist.UpdatedAt = DateTime.UtcNow;

            var result = unitOfWork.Users.Update(exist);

            response.Data = result.Adapt<UserForViewModel>();

            return response;
        }
    }
}
