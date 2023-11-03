using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MobileWeb.Data;
using Microsoft.AspNetCore.Identity;
using MobileWeb.Services;
using MobileWeb.Models.Entities;
using MobileWeb.Areas.Admin.Services.CategoryService;
using MobileWeb.Areas.Admin.Services.ProductService;
using MobileWeb.Areas.Identity.Services;

var builder = WebApplication.CreateBuilder(args);

// add Sqlite database context
builder.Services.AddDbContext<WebDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

// them data context cho identity
builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false; // ky tu dac biet
    options.Password.RequireDigit = false; // ky tu so
    options.Password.RequireUppercase = false; // ky tu in hoa
    options.Password.RequireLowercase = false; // ky tu thuong
    options.SignIn.RequireConfirmedAccount = false;
})
	.AddEntityFrameworkStores<UserDbContext>()
	.AddDefaultTokenProviders();

// them cookies cho xac thuc
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(20);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

// dang ky dich vu thao tac voi gio hang
builder.Services.AddTransient<CartService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAccountService, AccountService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
  name: "area",
  pattern: "{area:exists}/{controller}/{action}/{id?}");

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
