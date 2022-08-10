using Instagram.Domain.Commons.Responses;
using Instagram.Service.DTOs.UserDTOs;
using Instagram.Service.Interfaces.IUserServices;
using Instagram.Service.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }


        // success
        [HttpPost]
        public async Task<ActionResult<BaseResponse<UserForViewModel>>> CreateAsync(UserForCreation user)
        {
            var result = await _userService.CreateAsync(user);

            return StatusCode(result.Error is null
                ? 200
                : result.Error.Code
                , result);
        }

        
        
        // success
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<BaseResponse<UserForViewModel>>> GetAsync(Guid id)
        {
            var result = await _userService.GetAsync(user => user.Id == id);

            return StatusCode(result.Error is null
                ? 200
                : result.Error.Code
                , result);
        }

        // success
        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<UserForViewModel>>>> GetAllAsync
            (string partOfUsername, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var result = await _userService.GetAllAsync
                (user => user.Username.Contains(partOfUsername), Tuple.Create(pageIndex, pageSize));

            return StatusCode(result.Error is null
                ? 200
                : result.Error.Code
                , result);
        }

        // success
        [HttpPut]
        public async Task<ActionResult<BaseResponse<IEnumerable<UserForViewModel>>>> UpdateAsync([FromQuery] Guid id, UserForUpdate user)
        {
            var result = await _userService.UpdateAsync(id, user);

            return StatusCode(result.Error is null
                ? 200
                : result.Error.Code
                , result);
        }


        // success
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<BaseResponse<UserForViewModel>>> DeleteAsync(Guid id)
        {
            var result = await _userService.DeleteAsync(user => user.Id == id);

            return StatusCode(result.Error is null
                ? 200
                : result.Error.Code
                , result);
        }

        [HttpPost("changePassword")]
        public async Task<ActionResult<BaseResponse<UserForViewModel>>> ChangePasswordAsync
            (UserForChangePassword password)
        {
            var result = await _userService.ChangePasswordAsync(password);

            return StatusCode(result.Error is null
                ? 200
                : result.Error.Code
                , result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<UserForViewModel>>> LoginAsync(UserForLogin user)
        {
            var result = await _userService.CheckLoginAsync(user);

            return StatusCode(result.Error is null
                ? 200
                : result.Error.Code
                , result);
        }

    }
}
