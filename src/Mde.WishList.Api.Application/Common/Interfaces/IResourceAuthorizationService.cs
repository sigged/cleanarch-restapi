using System.Security.Claims;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Application.Common.Interfaces
{
    public interface IResourceAuthorizationService
    {
        Task<bool> Authorize(object resource, string policyName);
    }
}
