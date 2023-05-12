using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileWeb.Data;
using Microsoft.AspNetCore.Identity;
using MobileWeb.Areas.Identity.Data;
using MobileWeb.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MobileWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MobileWebContext") ?? throw new InvalidOperationException("Connection string 'MobileWebContext' not found.")));

builder.Services.AddDefaultIdentity<MobileWebUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MobileWebContext>()
                .AddDefaultTokenProviders();

//builder.Services.AddIdentity<MobileWebUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<MobileWebContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

builder.Services.AddAuthorization(option =>
{
  option.AddPolicy("AllowAddToCartAndBuyProduct", policyBuilder =>
  {
    policyBuilder.RequireAuthenticatedUser();
    policyBuilder.RequireRole("user");
  });
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromMinutes(20);
  options.Cookie.HttpOnly = true;
  options.Cookie.IsEssential = true;
});

builder.Services.AddTransient<CartService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapRazorPages();

app.MapControllerRoute(
  name: "MyArea",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

//app.MapAreaControllerRoute(
//    name: "MyAreaAdmin",
//    areaName: "Admin",
//    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

/*app.UseEndpoints(endpoints =>
{
  endpoints.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

  endpoints.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller}/{action}");

  *//*endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?");*//*
});*/

app.Run();
