using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MobileWeb.Models.DTOs;
using MobileWeb.Models.Entities;
using MobileWeb.Services.CartService;
using MobileWeb.Services.UserService;

namespace MobileWeb.Controllers;

[Route("user")]
public class UserController : Controller
{
	private readonly ICartService _cartService;
	private readonly UserManager<User> _userManager;
	private readonly IUserService _userService;

	public UserController(ICartService cartService, UserManager<User> usermanager, IUserService userService)
	{
		_cartService = cartService;
		_userManager = usermanager;
		_userService = userService;
	}

	public IActionResult Index()
	{
		return View();
	}

	[Route("check-out")]
	public async Task<IActionResult> CheckOutAsync()
	{
		return View();
	}
}
