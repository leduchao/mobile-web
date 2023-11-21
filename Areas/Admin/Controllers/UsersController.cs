
using Microsoft.AspNetCore.Mvc;
using MobileWeb.Services.UserService;

namespace MobileWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllUsersAsync();
        return View(users);
    }

    [Route("delete-user")]
    public async Task<IActionResult> DeleteUser(string uid)
    {
        var result = await _userService.DeleteUserAsync(uid);

        if (result)
            return RedirectToAction(nameof(Index));

        return View();
    }
}
