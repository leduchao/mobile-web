using MobileWeb.Areas.Identity.Models.Account;
using MobileWeb.Models.Entities;

namespace MobileWeb.Areas.Identity.Services
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<bool> RegisterAsync(RegisterViewModel model);

        Task ChangePasswordAsync(ResetPasswordViewModel model);

        Task SendEmailAsync(string to, string callbackUrl);

        Task<bool> ConfirmEmailAsync(string uid, string token);

        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(string id);

        Task DeleteUserByEmailAsync(string email);

        Task<string> GenerateEmailConfirmTokenAsync(User user);
    }
}
