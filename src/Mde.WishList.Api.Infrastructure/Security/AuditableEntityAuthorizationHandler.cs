using Mde.WishList.Api.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Infrastructure.Security
{
    public class AuditableEntityAuthorizationHandler : AuthorizationHandler<SameCreatorRequirement, AuditableEntity>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       SameCreatorRequirement requirement,
                                                       AuditableEntity resource)
        {
            if (context.User.Identity.IsAuthenticated &&
                context.User.FindFirstValue(ClaimTypes.NameIdentifier) == resource.CreatedBy)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class SameCreatorRequirement : IAuthorizationRequirement { }
}