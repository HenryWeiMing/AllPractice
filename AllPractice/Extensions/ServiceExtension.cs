using AllPractice.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace AllPractice.Extensions
{
    public static class ServiceExtension
    {
        public static void AddService(this IServiceCollection service)
        {
            var serviceProvider = service.BuildServiceProvider();
            var appSetting = serviceProvider.GetRequiredService<IOptions<AppSetting>>().Value;
            service.AddMemoryCache();
            service.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(appSetting.URL.Redis));

            service.AddScoped<SqlConnection>(ServiceProvider =>
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = appSetting.URL.DB;
                return conn;
            });

            service.AddDbContext<W1Context>(
                options => options.UseSqlServer(appSetting.URL.DB));

            service.AddDbContext<_1234Context>(
                options => options.UseNpgsql(appSetting.URL.Npgsql));

            service.AddScoped<CacheModel>();
        }
    }
}
