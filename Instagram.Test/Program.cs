using Instagram.Service.Interfaces.IUserServices;
using Instagram.Service.Services.UserServices;

IUserService userService = new UserService();

var response = await userService.ChangePasswordAsync(new()
{
    UsernameOrEmail = "muhammad",
    OldPassword = "muhammad",
    NewPassword = "Muhammad",
    ConfirmPassword = "Muhammad"    
});

Console.WriteLine(response?.Error.Code + " " + response?.Error.Message + " " + response?.Data.FullName);