using CafeDotNet.Infra.Bootstraper.Helpers;
using CafeDotNet.Infra.Data.Common.Context;
using CafeDotNet.Infra.Data.Common.SeedHelpers;
using CafeDotNet.Infra.Logging.Helpers;
using CafeDotNet.Infra.Mail.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddCustomSerilog();

builder.Services.AddControllersWithViews();

builder.Services.AddCustomEmailSettings(builder.Configuration);
builder.Services.AddDatabaseProvider(builder.Configuration);

builder.Services.RegisterEmailServices();
builder.Services.RegisterCoreServices();
builder.Services.RegisterDatabaseServices();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";  
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CafeDbContext>();    
    await UserSeed.SeedAsync(dbContext); 
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomSerilog();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
