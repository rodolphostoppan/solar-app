using SolarApp.Services;
using Microsoft.AspNetCore.ResponseCompression;

namespace Db1HealthPanelBack.Configs
{
    public static class ServicesConfig
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ClientService>();
            services.AddScoped<BillService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<SizingService>();
        }

        public static void AddCompressionToResponse(this IServiceCollection services)
            => services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = new[]
            {
                "text/plain",
                "text/css",
                "application/javascript",
                "text/html",
                "application/xml",
                "text/xml",
                "application/json",
                "text/json"
            };
        });
    }
}