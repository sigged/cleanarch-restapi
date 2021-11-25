using Mde.WishList.Api.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Infrastructure.Security
{
    //ref: https://docs.microsoft.com/en-us/aspnet/core/security/authorization/resourcebased?view=aspnetcore-6.0

    public class ResourceAuthorizationService : IResourceAuthorizationService
    {
        protected readonly IAuthorizationService _authorizationService;
        protected readonly HttpContext _context;

        public ResourceAuthorizationService(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
            _context = httpContextAccessor.HttpContext;
        }

        public async Task<bool> Authorize(object resource, string policy)
        {
            var result = await _authorizationService.AuthorizeAsync(_context.User, resource, policy);
            return result.Succeeded;
        }

        public async Task<bool> AuthorizeAll(object resource, params string[] policies)
        {
            bool authorized = true;
            foreach(var policy in policies)
            {
                authorized = await Authorize(resource, policy);
            }
            return authorized;
        }

        public async Task<bool> AuthorizeAny(object resource, params string[] policies)
        {
            foreach (var policy in policies)
            {
                if ((await Authorize(resource, policy)) == true) return true;
            }
            return false;
        }
    }
}
