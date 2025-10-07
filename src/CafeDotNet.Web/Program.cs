using CafeDotNet.Infra.Logging.Helpers;
using CafeDotNet.Infra.Mail.Helpers;
using CafeDotNet.Infra.Bootstraper.Helpers; 

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddCustomSerilog();

builder.Services.AddControllersWithViews();

builder.Services.AddCustomEmailSettings(builder.Configuration);
builder.Services.AddDatabaseProvider(builder.Configuration);

builder.Services.RegisterEmailServices();
builder.Services.RegisterDatabaseServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseCustomSerilog();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
