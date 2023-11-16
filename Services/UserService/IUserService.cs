using MobileWeb.Models.DTOs;
using MobileWeb.Models.Entities;

namespace MobileWeb.Services.UserService;

public interface IUserService
{
	Task<bool> Order(string uid, OrderDTO orderDTO);
}
