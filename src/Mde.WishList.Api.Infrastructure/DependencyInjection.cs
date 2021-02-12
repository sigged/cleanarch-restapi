using Mde.WishList.Api.Application.Common.Interfaces;
using Mde.WishList.Api.Infrastructure.Files;
using Mde.WishList.Api.Infrastructure.Identity;
using Mde.WishList.Api.Infrastructure.Persistence;
using Mde.WishList.Api.Infrastructure.Security;
using Mde.WishList.Api.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // configure strongly typed settings objects
            var jwtSettingsSection = configuration.GetSection("Settings:Jwt");
            services.Configure<TokenSettings>(jwtSettingsSection);
            var tokenSettings = jwtSettingsSection.Get<TokenSettings>();

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("Mde.WishList.ApiDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            var tokenValidationParmFactory = new TokenValidationParametersFactory(tokenSettings);
            services.AddSingleton<ITokenValidationParametersFactory>(tokenValidationParmFactory);
            var tokenValidationParms = tokenValidationParmFactory.GetDefaultValidationParameters();

            services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
            .AddJwtBearer(jwtOptions => {
                jwtOptions.ClaimsIssuer = tokenSettings.JwtIssuer;
                jwtOptions.TokenValidationParameters = tokenValidationParms;
                jwtOptions.SaveToken = true;
                jwtOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            //used to easily detect an expired token by the client
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            return services;
        }
    }
}