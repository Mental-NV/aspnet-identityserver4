using Microsoft.IdentityModel.Tokens;
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
            
            string authenticationProviderKey = "IdentityApiKey";
            builder.Services.AddAuthentication()
                .AddJwtBearer(authenticationProviderKey, x =>
                {
                    x.Authority = "https://localhost:5005";
                    // x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });


            builder.Services.AddOcelot();
            var app = builder.Build();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await app.UseOcelot();
            await app.RunAsync();
        }
    }
}