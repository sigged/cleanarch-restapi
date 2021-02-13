using Mde.WishList.Api.Application.Common.Models;
using Mde.WishList.Api.Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<IUser> FindByName(string userName);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<Result> AuthenticateAsync(string userName, string password);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<IEnumerable<Claim>> GetClaims(IUser user);
    }
}
