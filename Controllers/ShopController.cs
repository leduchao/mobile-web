using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MobileWeb.Areas.Admin.Services.ProductService;
using MobileWeb.Models;
using MobileWeb.Models.DTOs;
using MobileWeb.Models.Entities;
using MobileWeb.Services.CartService;
using MobileWeb.Services.UserService;

namespace MobileWeb.Controllers;

[Route("shop")]
public class ShopController : Controller
{
	private readonly IProductService _productService;
	private readonly ICartService _cartService;
	private readonly IUserService _userService;
	private readonly UserManager<User> _userManager;

	public ShopController(IProductService producService, ICartService cartService,
		UserManager<User> userManager, IUserService userService)
	{
		_productService = producService;
		_cartService = cartService;
		_userManager = userManager;
		_userService = userService;
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> Index(string keyword, string price, int page)
	{
		var productList = _productService.GetAllAsync();


		if (!string.IsNullOrEmpty(keyword))
		{
			productList = productList.Where(p =>
				p.Name!.Contains(keyword) || p.Category!.Name!.Contains(keyword));
		}

		if (!string.IsNullOrEmpty(price))
		{
			switch (price)
			{
				case "min-to-max":
                    productList = productList.OrderBy(p => p.Price);
					break;

				case "max-to-min":
                    productList = productList.OrderByDescending(p => p.Price);
					break;

				default:
					break;
			}
		}

		if (page < 1) page = 1;
		int pageSize = 12;

		ViewBag.Keyword = keyword;
		ViewBag.Price = price;
		var paginatedList = await PaginatedList<Product>.CreateAsync(productList, page, pageSize);
			
		return View(paginatedList);
	}

	[Route("product-detail")]
	[AllowAnonymous]
	public async Task<IActionResult> ProductDetails(int id)
	{
		var product = await _productService.GetByIdAsync(id);

        if (product == null)
			return NotFound();

        var productImages = await _productService.GetAllProductImagesAsync(product);

		ViewBag.Specifications = await _productService.GetSpecificationsAsync(product.Id);
        ViewBag.ProductImages = productImages;
		return View(product);
	}

	//dat hang
	[Route("checkout")]
	[Authorize(Roles = "Customer")]
	public async Task<IActionResult> Checkout()
	{
		if (User.Identity == null)
			return NotFound();

		var user = await _userManager.FindByNameAsync(User.Identity.Name);

		if (user == null)
			return NotFound();

		ViewBag.UserId = user.Id;
		ViewBag.UserFirstName = user.FirstName;
		ViewBag.UserLastName = user.LastName;
		ViewBag.UserAddress = user.Address;
		ViewBag.UserPhoneNumber = user.PhoneNumber;

		return View();
	}

	//dat hang
	[Route("checkout")]
	[Authorize(Roles = "Customer")]
	[HttpPost]
	public async Task<IActionResult> Checkout(string uid, OrderDTO orderDTO)
	{
		if (ModelState.IsValid)
		{
			var result = await _userService.Order(uid, orderDTO);

			if (result)
				return RedirectToAction(nameof(OrderSuccess));
			else
				ModelState.AddModelError(
					"OrderFailed", "Không thể đặt hàng! Vui lòng kiểm tra lại thông tin!");
		}

		return View();
	}

	[Route("order-success")]
	public IActionResult OrderSuccess()
	{
		return View();
	}

	[HttpPost]
	[Authorize(Roles = "Customer")]
	public IActionResult Buy()
	{
		return View();
	}

	//sap xep theo gia
	//public IActionResult SortProductsByPrice(string price)
	//{
	//	var listProduct =  _productService.GetAllAsync();
	//	IEnumerable<Product> listSorted;

	//	if (price == "min-to-max")
	//		listSorted = listProduct.OrderBy(p => p.Price);
	//	else if (price == "max-to-min")
	//		listSorted = listProduct.OrderByDescending(p => p.Price);
	//	else
	//		return RedirectToAction(nameof(Index));

	//	return View(nameof(Index), listSorted);
	//}

	[Route("cart")]
	[AllowAnonymous]
	public IActionResult ShowCart()
	{
		return View(_cartService.GetAllItems());
	}

	[Route("add-to-cart")]
	[Authorize(Roles = "Customer")]
	[HttpGet]
	public async Task<IActionResult> AddToCartAsync(int pid)
	{
		var product = await _productService.GetByIdAsync(pid);

		if (product == null)
			return NotFound();

		var cart = _cartService.GetAllItems();

		var cartItem = cart.FirstOrDefault(p => p.Product.Id == pid);

		if (cartItem != null)
			cartItem.Quantity++;
		else
			cart.Add(new CartItem() { Quantity = 1, Product = product });

		_cartService.SaveCartSession(cart);

		return RedirectToAction(nameof(ShowCart));
	}

	[Route("clear-cart")]
	[AllowAnonymous]
	public IActionResult ClearCart()
	{
		_cartService.ClearCart();
		return RedirectToAction(nameof(ShowCart));
	}

	[Route("remove-one")]
	[AllowAnonymous]
	public IActionResult RemoveItemById(int id)
	{
		var cartItems = _cartService.GetAllItems();

		var item = cartItems.FirstOrDefault(i => i.Product.Id == id);

		if (item != null)
		{
			cartItems.Remove(item);
			_cartService.SaveCartSession(cartItems);
		}

		return RedirectToAction(nameof(ShowCart));
	}

	[Route("update-cart")]
	[AllowAnonymous]
	[HttpPost]
	public IActionResult UpdateCart(int pid, int quantity)
	{
		var cart = _cartService.GetAllItems();

		var cartItem = cart.FirstOrDefault(p => p.Product.Id == pid);

		if (cartItem != null)
		{
			if (cartItem.Quantity < 1)
			{
				cartItem.Quantity = 1;
			}
			else
				cartItem.Quantity = quantity;
		}

		_cartService.SaveCartSession(cart);

		return RedirectToAction(nameof(ShowCart));
	}
}
