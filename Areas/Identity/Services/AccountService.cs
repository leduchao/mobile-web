using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using MobileWeb.Areas.Identity.Models.Account;
using MobileWeb.Data;
using MobileWeb.Models.Entities;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MobileWeb.Areas.Identity.Services;

public class AccountService : IAccountService
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signinManager;
	//private readonly UserDbContext _context;
	private readonly RoleManager<IdentityRole> _roleManager;

	public AccountService(
		UserManager<User> userManager,
		SignInManager<User> signinManager,
		UserDbContext context,
		RoleManager<IdentityRole> roleManager)
	{
		_userManager = userManager;
		_signinManager = signinManager;
		//_context = context;
		_roleManager = roleManager;
	}

	public Task ChangePasswordAsync(ResetPasswordViewModel model)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> LoginAsync(LoginViewModel model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);

		if (user is not null)
		{
			var result = await _signinManager.PasswordSignInAsync(user.UserName, model.Password, 
				model.RememberMe, false);

			return result.Succeeded;
		}

		return false;
	}

	public async Task LogoutAsync()
	{
		await _signinManager.SignOutAsync();
	}

	public async Task<bool> RegisterAsync(RegisterViewModel model)
	{
		var newUser = new User
		{
			UserName = model.UserName,
			Email = model.Email,
			CreatedAt = DateTime.Now,
		};

		var result = await _userManager.CreateAsync(newUser, model.Password);

		if (result.Succeeded)
		{
			await AddRoleAsync(newUser);
			return result.Succeeded;
		}

		return false;
	}

	private async Task AddRoleAsync(User user)
	{
		if (!await _roleManager.RoleExistsAsync("Admin"))
		{
			await _roleManager.CreateAsync(new IdentityRole("Admin"));
			await _userManager.AddToRoleAsync(user, "Admin");
		}
		else
		{
			var usersWithAdminRole = await _userManager.GetUsersInRoleAsync("Admin");

			if (usersWithAdminRole.Count == 0)
				await _userManager.AddToRoleAsync(user, "Admin");
			else
			{
				if (!await _roleManager.RoleExistsAsync("Customer"))
					await _roleManager.CreateAsync(new IdentityRole("Customer"));

				await _userManager.AddToRoleAsync(user, "Customer");
			}
		}
	}

	public async Task SendEmailAsync(string to, string callbackUrl)
	{
		var mailSettings = new MailSettings()
		{
			Subject = "Xác thực email",
			Body = $"Hãy <a href='{callbackUrl}'>click vào đây</a> để xác thực email!"
        };

		using var client = new SmtpClient(mailSettings.Host)
		{
			Port = mailSettings.Port,
			Credentials = new NetworkCredential(mailSettings.From, mailSettings.Password),
			EnableSsl = true
		};

		try
		{
			var message = new MailMessage(mailSettings.From, to, mailSettings.Subject, mailSettings.Body)
			{
				IsBodyHtml = true
			};

			await client.SendMailAsync(message);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
	}

	public async Task<bool> ConfirmEmailAsync(string uid, string token)
	{
		var user = await _userManager.FindByIdAsync(uid);

		if (user == null) return false;

		token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
		var result = await _userManager.ConfirmEmailAsync(user, token);

		return result.Succeeded;
	}

	public async Task<User> GetUserByEmail(string email)
	{
		return await _userManager.FindByEmailAsync(email);
	}

	public async Task DeleteUserByEmailAsync(string email)
	{
		var user = await _userManager.FindByEmailAsync(email);

		if (user is not null)
			await _userManager.DeleteAsync(user);
	}

	public async Task<string> GenerateEmailConfirmTokenAsync(User user)
	{
		string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
		token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

		return token;
	}

	public async Task<User> GetUserById(string id)
	{
		return await _userManager.FindByIdAsync(id);
	}
}
