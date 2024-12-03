using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// Application-level event: On Startup
app.Lifetime.ApplicationStarted.Register(() =>
{
    Console.WriteLine("Application Started");
});

// Application-level event: On Stopping
app.Lifetime.ApplicationStopping.Register(() =>
{
    Console.WriteLine("Application Stopping");
});

// Configure middleware
app.UseRouting();
app.UseSession();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

});

// Handle global errors
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unhandled exception: {ex.Message}");
        await context.Response.WriteAsync("An error occurred.");
    }
});

app.Run();
