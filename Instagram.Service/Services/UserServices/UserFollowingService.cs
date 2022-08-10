using Instagram.Data.IRepositories;
using Instagram.Data.Repositories;
using Instagram.Domain.Commons.Responses;
using Instagram.Domain.Entities.Users;
using Instagram.Service.DTOs.UserFollowingDTOs;
using Instagram.Service.Extentions;
using Instagram.Service.Interfaces.IUserServices;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Instagram.Service.Services.UserServices
{
#pragma warning disable
    public class UserFollowingService : IUserFollowingService
    {
        private readonly IUserFollowingRepository _userFollowingRepository;

        private readonly TypeAdapterConfig config;

        public UserFollowingService()
        {
            _userFollowingRepository = new UserFollowingRepository();

            config = new TypeAdapterConfig();
            config.NewConfig<UserFollowing, UserFollowingForViewModel>()
                .Map(dest => dest.FollowingTo, src => src.Adapt<UserFollowingForViewModel>());
        }

        public async Task<BaseResponse<bool>> CreateAsync(UserFollowingForCreation follow)
        {
            var response = new BaseResponse<bool>();


            var exist = await _userFollowingRepository.GetAsync
                (x => x.FollowTo == follow.FollowTo && x.FollowFrom == follow.FollowFrom);

            if (exist is not null)
            {
                _userFollowingRepository.DeleteAsync
                    (x => x.FollowTo == follow.FollowTo && x.FollowFrom == follow.FollowFrom);

                response.Data = false;

                return response;
            }

            var userFollowing = await _userFollowingRepository.CreateAsync(follow.Adapt<UserFollowing>());

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<UserFollowingForViewModel>>> GetAllAsync
            (Expression<Func<UserFollowing, bool>> expression, Tuple<int, int> pagenation = null)
        {
            var response = new BaseResponse<IEnumerable<UserFollowingForViewModel>>();

            response.Data = _userFollowingRepository.GetAll(expression)
                                .GetWithPagenation(pagenation)
                                    .Include(follow => follow.FollowingTo)
                                            .Adapt<IEnumerable<UserFollowingForViewModel>>(config);

            return response;
        }
    }
}
