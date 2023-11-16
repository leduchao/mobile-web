using Azure.Core;
using Microsoft.AspNetCore.Identity;
using MobileWeb.Areas.Admin.Services.ProductService;
using MobileWeb.Areas.Identity.Services;
using MobileWeb.Data;
using MobileWeb.Models.DTOs;
using MobileWeb.Models.Entities;
using MobileWeb.Services.CartService;
using System.Transactions;

namespace MobileWeb.Services.UserService;

public class UserService : IUserService
{
    private readonly WebDbContext _dbContext;
    private readonly ICartService _cartService;
    private readonly UserManager<User> _userManager;

    public UserService(WebDbContext dbContext, ICartService cartService, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _cartService = cartService;
        _userManager = userManager;
    }

    public async Task<bool> Order(string uid, OrderDTO orderDTO)
    {
        var cart = _cartService.GetAllItems();
        var user = await _userManager.FindByIdAsync(uid);

        if (user != null)
        {
            try
            {
                user.FirstName = orderDTO.Firstname;
                user.LastName = orderDTO.LastName;
                user.Address = orderDTO.Address;
                user.PhoneNumber = orderDTO.PhoneNumber;

                var newOrder = new Order
                {
                    Status = Status.Processing,
                    UserId = user.Id,
                    PaymentMethod = orderDTO.PaymentMethod,
                    OrderAt = DateTime.Now,
                    DeliveryAt = DateTime.Now.AddDays(4d)
                };

                foreach (var item in cart)
                {
                    var newOrderItem = new OrderItem
                    {
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.Product.Price,
                        Order = newOrder
                    };

                    newOrder.OrderItems.Add(newOrderItem);
                }

                await _userManager.UpdateAsync(user);
                _dbContext.Orders.Add(newOrder);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        return false;
    }
}
