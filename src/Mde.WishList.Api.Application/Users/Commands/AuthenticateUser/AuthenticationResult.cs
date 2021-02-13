using Mde.WishList.Api.Application.Common.Models;
using System.Collections.Generic;

namespace Mde.WishList.Api.WebApi.V1.Users.Dto
{
    public class AuthenticationResult : Result
    {
        public AuthenticationResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {

        }

        public string Token { get; set; }
    }
}
