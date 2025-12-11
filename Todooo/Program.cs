using Todooo.Services;
using Todooo.Filters;
using Microsoft.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddProvider(new FileLoggerProvider("Logs/app_log.txt"));

builder.Services.AddScoped<LogActionFilter>();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.AddService<LogActionFilter>();
});
builder.Services.AddSession();////
builder.Services.AddScoped<ISessionManagerService, SessionManagerService>();
builder.Services.AddScoped<IAuthService, AuthService>();



var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();////
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}")

    .WithStaticAssets();


app.Run();
