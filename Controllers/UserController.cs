using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MobileWeb.Areas.Identity.Services;
using MobileWeb.Models.DTOs;
using MobileWeb.Models.Entities;
using MobileWeb.Services.CartService;
using MobileWeb.Services.EmailService;
using MobileWeb.Services.UserService;
using System.Reflection.Metadata.Ecma335;

namespace MobileWeb.Controllers;

[Route("user")]
public class UserController : Controller
{
	private readonly ICartService _cartService;
	private readonly IUserService _userService;
	private readonly IEmailService _emailService;
	private readonly IAccountService _accountService;

	public UserController(ICartService cartService, IUserService userService, IEmailService emailService,
		IAccountService accountService)
	{
		_cartService = cartService;
		_userService = userService;
		_emailService = emailService;
		_accountService = accountService;
	}

	public async Task<IActionResult> Index(string uid)
	{
		var user = await _userService.FindUserByIdAsync(uid);

		if (user is null)
			return NotFound();

		ViewBag.ProcessingOrderList = await _userService.GetProccessingOrdersAsync(uid);
		ViewBag.ShippingOrderList = await _userService.GetShippingOrdersAsync(uid);
		ViewBag.FinishedOrderList = await _userService.GetFinishedOrdersAsync(uid);
		return View(user);
	}

	[Route("edit-profile")]
	public async Task<IActionResult> EditProfile(string uid)
	{
		var user = await _userService.FindUserByIdAsync(uid);

		if (user is null)
			return NotFound();

		user.FirstName ??= "null";
		user.LastName ??= "null";
		user.Address ??= "null";
		user.Birthday ??= new DateTime(2023, 01, 01);
		user.Avatar ??= "avatar1.png";

		var userDTO = new UserDTO
		{
			UserName = user.UserName,
			FirstName = user.FirstName,
			LastName = user.LastName,
			Email = user.Email,
			Address = user.Address,
			PhoneNumber = user.PhoneNumber,
			Birthday = user.Birthday,
			//AvatarUrl = user.Avatar
		};

		ViewBag.UserId = user.Id;
		ViewBag.AvatarUrl = user.Avatar;

		return View(userDTO);
	}

	[HttpPost]
	[Route("edit-profile")]
	public async Task<IActionResult> EditProfile(string uid, UserDTO userDTO)
	{
		var result = await _userService.UpdateUserAsync(uid, userDTO);

		if (result)
			return RedirectToAction(nameof(Index), new { uid });

		ModelState.AddModelError("UpdateUserError", "Không thể cập nhật thông tin! Vui lòng kiểm tra lại!");
		return View();
	}

	[Route("verify-email")]
	public async Task<IActionResult> VerifyEmail(string email)
	{
		var user = await _userService.FindUserByEmailAsync(email);

		if (user is null)
			return NotFound();

		var token = await _accountService.GenerateEmailConfirmTokenAsync(user);
		var link = Url.Action("ConfirmEmail", "User", new { uid = user.Id, token }, Request.Scheme);

		if (!string.IsNullOrEmpty(link))
		{
			await _emailService.SendMailAsync(user.Email, "Xác thực email", link);
			return RedirectToAction(nameof(ConfirmEmail));
		}

		ModelState.AddModelError("ConfirmFailed", "Không thể xác thực email!");
		return View();
	}

	[Route("confirm-email")]
	public IActionResult ConfirmEmail()
	{
		return View();
	}

	[HttpGet]
	[Route("confirm-email")]
	public async Task<IActionResult> ConfirmEmail(string uid, string token)
	{
		var result = await _accountService.ConfirmEmailAsync(uid, token);

		if (result)
			return RedirectToAction(nameof(Index), new { uid });

		ModelState.AddModelError("ConfirmFaile", "Không thể các thực email!");
		return View();
	}

	[Route("delete-email")]
	public async Task<IActionResult> DeleteUser(string uid)
	{
		var result = await _userService.DeleteUserAsync(uid);

		if (result)
		{
			await _accountService.LogoutAsync();
			return RedirectToAction("Index", "Home");
		}

		ModelState.AddModelError("ConfirmFaile", "Không thể xóa tài khoản!");
		return View();
	}

	[Route("change-password")]
	public async Task<IActionResult> ChangePassword(string uid)
	{
		var user = await _userService.FindUserByIdAsync(uid);

		if (user is not null)
		{
			var token = await _accountService.GenerateForgotPaswordTokenAsync(user);
			var link = Url.Action("ResetPassword", "Account", new { area = "Identity", uid }, Request.Scheme);

			if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(link))
			{
				string body = $"Hãy <a href='{link}'>Click vào đây</a> để đổi mật khẩu!";
				await _emailService.SendMailAsync(user.Email, "Đổi mật khẩu", body);

				return RedirectToAction("ConfirmResetPassword", "Account", new { area = "Identity" });
			}
		}

		ModelState.AddModelError("ConfirmFaile", "Không thể đổi mật khẩu!");
		return View();
	}

	[Route("cancel-order")]
	public async Task<IActionResult> CancelOrder(int oid, string uid)
	{
		var result = await _userService.CancelOrder(oid);

		if (result is false)
		{
			ViewBag.CancelSuccess = false;
		}

		ViewBag.CancelSuccess = true;
		return RedirectToAction("Index", new { uid });
	}
}