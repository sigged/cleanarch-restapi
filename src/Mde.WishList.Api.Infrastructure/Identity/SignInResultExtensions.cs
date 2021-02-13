using Mde.WishList.Api.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Mde.WishList.Api.Infrastructure.Identity
{
    public static class SignInResultExtensions
    {
        private const string LockedOut = "User is locked out";
        private const string RequiresTwoFactor = "User requires two factor authentication";
        private const string NotAllowed = "User not allowed";
        private const string Failed = "Invalid credentials";

        public static Result ToApplicationResult(this SignInResult result)
        {
            if (result.Succeeded)
            {
                return Result.Success();
            }
            else if(result.IsLockedOut)
            {
                return Result.Failure(new[] { LockedOut });
            }
            else if (result.IsNotAllowed)
            {
                return Result.Failure(new[] { NotAllowed });
            }
            else if (result.RequiresTwoFactor)
            {
                return Result.Failure(new[] { RequiresTwoFactor });
            }
            else
            {
                return Result.Failure(new[] { Failed });
            }
        }
    }
} 