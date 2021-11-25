using Mde.WishList.Api.Application.Common.Interfaces;
using Mde.WishList.Api.Application.Common.Security;
using Mde.WishList.Api.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Infrastructure.Security
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options => {
                options.AddPolicy(Policies.MustBeAdmin, policy => policy.RequireRole("Administrator"));
                options.AddPolicy(Policies.MustBeCreator, policy => policy.Requirements.Add(new MustBeCreatorRequirement()));
                options.AddPolicy(Policies.MustBeLastModifier, policy => policy.Requirements.Add(new MustBeLastModifierRequirement()));
            });

            services.AddTransient<IResourceAuthorizationService, ResourceAuthorizationService>();
            services.AddSingleton<IAuthorizationHandler, MustBeLastModifierAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, MustBeCreatorAuthorizationHandler>();

            return services;
        }

    }

    public class MustBeCreatorAuthorizationHandler : AuthorizationHandler<MustBeCreatorRequirement, AuditableEntity>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MustBeCreatorRequirement requirement,
                                                       AuditableEntity resource)
        {
            if (context.User.FindFirstValue(ClaimTypes.NameIdentifier) == resource.CreatedBy)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class MustBeLastModifierAuthorizationHandler : AuthorizationHandler<MustBeLastModifierRequirement, AuditableEntity>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MustBeLastModifierRequirement requirement,
                                                       AuditableEntity resource)
        {
            if (context.User.FindFirstValue(ClaimTypes.NameIdentifier) == resource.LastModifiedBy)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class MustBeCreatorRequirement : IAuthorizationRequirement { }
    public class MustBeLastModifierRequirement : IAuthorizationRequirement { }
}
