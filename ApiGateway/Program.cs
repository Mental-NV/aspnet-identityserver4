using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("ocelot.json");
            });
            builder.Services.AddOcelot();
            var app = builder.Build();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await app.UseOcelot();
        }
    }
}