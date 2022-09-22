using Microsoft.EntityFrameworkCore;
using jober.Data;
using jober.services;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<dbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection")));


builder.Services.AddDbContext<IdentityContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection")));
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<items, names>();
//builder.Services.AddSingleton<items, names>();
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

//app.Use((context, Next) =>
//{   if (context.Request.Path == "/tesat-admin")
//    {
//        items kk =context.RequestServices.GetRequiredService<items>();
//        context.Response.ContentType = "text/plain";
//        context.Response.WriteAsync(kk.saymyname());
//    }

//    return Next();
//});
//app.MapGet("/test-admin", (HttpContext context,items pp,IConfiguration config) =>
//{
//    string values = "martins dependency";

//    if (app.Environment.IsDevelopment())
//    {
//        values = "jones";
//        var hh = int.Parse(context.Request.Cookies["user"]?? "0")  + 1;

//    }
//    return context.Response.WriteAsync(pp.saymyname()+values+" " + config["Logging:LogLevel:Default"]);
//});

//app.MapPost("/admin", (HttpContext context) =>
//{

//    var products = JsonSerializer.Deserialize<names>(context.Request.Body);
//    context.Response.StatusCode = StatusCodes.Status200OK;

//});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "Areas", pattern: "{area:exists}/{controller=Users}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
