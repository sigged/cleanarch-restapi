using System.Security.Claims;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Application.Common.Interfaces
{
    public interface IResourceAuthorizationService
    {
        /// <summary>
        /// Authorizes access to the specified resource if policy is met
        /// </summary>
        /// <returns></returns>
        Task<bool> Authorize(object resource, string policy);

        /// <summary>
        /// Authorizes access to the specified resource if at least one of the policies is met
        /// </summary>
        /// <returns></returns>
        Task<bool> AuthorizeAny(object resource, params string[] policies);

        /// <summary>
        /// Authorizes access to the specified resource if all of the policies are met
        /// </summary>
        /// <returns></returns>
        Task<bool> AuthorizeAll(object resource, params string[] policies);
    }
}
