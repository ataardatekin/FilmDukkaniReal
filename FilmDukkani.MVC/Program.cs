using FilmDukkani.DAL.Context;
using FilmDukkani.Entity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authentication.Cookies;


using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using FilmDukkani.Entity.Base;
using FilmDukkani.IOC.Container;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


//Context

builder.Services.AddDbContext<FilmDukkaniContext>(options=>options.UseSqlServer(connectionString));

//Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<FilmDukkaniContext>()
    .AddDefaultTokenProviders();


//Session
builder.Services.AddSession(x =>
{
    x.Cookie.Name = "cart";
    x.IdleTimeout = TimeSpan.FromMinutes(5);

});


//Cookie
builder.Services.ConfigureApplicationCookie(x =>
{

    x.LoginPath = new PathString("/Account/Login");
    x.LogoutPath = new PathString("/Account/Logout");
    x.Cookie = new CookieBuilder()
    {
        Name = "LoginCookie",
        
    };
    x.SlidingExpiration = true;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    x.AccessDeniedPath = new PathString("/Account/AccessDenied");
});



FilmDukkani.IOC.Container.ServiceContainer.ServiceConfigure(builder.Services);









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


app.UseAuthentication();
app.UseAuthorization();

app.UseSession();



app.UseEndpoints(endpoints =>
{

    //Employee

    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );


    // Account/Login ve Account/Register route'larý
    endpoints.MapControllerRoute(
        name: "account",
        pattern: "{controller=Account}/{action=Login}/{id?}");


    // CancelOrder route'u
    endpoints.MapControllerRoute(
        name: "cancelorder",
        pattern: "Account/CancelOrder/{id:int}",
        defaults: new { controller = "Account", action = "CancelOrder" });


    //Default
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );

    //Details
    endpoints.MapControllerRoute(
    name: "details",
    pattern: "Home/Details/{id:int}",
    defaults: new { controller = "Home", action = "Details" }
);


});





app.Use(async (context, next) =>
{
    if (!context.User.Identity.IsAuthenticated && context.Request.Path != "/Account/Login" && context.Request.Path != "/Account/Register")
    {
        // Kullanýcý giriþ yapmamýþ ve istek yolu "/Account/Login" veya "/Account/Register" deðilse, "/Account/Login" sayfasýna yönlendir
        context.Response.Redirect("/Account/Login");
        return;
    }

    await next();
});

app.Run();
