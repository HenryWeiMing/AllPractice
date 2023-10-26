using AllPractice.Extensions;
using AllPractice.Models;
using Serilog;

namespace AllPractice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.Configure<AppSetting>(
                builder.Configuration.GetSection("AppSetting"));

            builder.Services.AddService();

            builder.Host.UseSerilog((ctx, lc) =>
                lc.ReadFrom.Configuration(ctx.Configuration));

            var app = builder.Build();

            app.Use(async (context, next) =>
            {
                Log.Information("Request Path:\t" + context.Request.Path);
                Log.Information("Request QueryString:\t" + context.Request.QueryString);
                await next.Invoke();
            });

            app.UseSerilogRequestLogging();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UsePathBase("/RProxy");
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=index}/{id?}");

            app.Run();
        }
    }
}