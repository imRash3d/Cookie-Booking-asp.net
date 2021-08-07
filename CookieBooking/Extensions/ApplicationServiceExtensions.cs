using CookieBooking.Infrastructure.Contracts;
using CookieBooking.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Extensions
{
    public static class  ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
           // services.AddSingleton<IConfigurationService, ConfigurationService>();
      
            return services;
        }
    }
}
