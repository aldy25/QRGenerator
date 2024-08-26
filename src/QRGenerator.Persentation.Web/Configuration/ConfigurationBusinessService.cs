using QRGenerator.Business.Interface;
using QRGenerator.Business.Repositories;

namespace QRGenerator.Persentation.Web.Configuration
{
    public static class ConfigurationBusinessService
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQRGenerateRepository, QRGenerateRepository>();
            return services;
        }
    }
}
