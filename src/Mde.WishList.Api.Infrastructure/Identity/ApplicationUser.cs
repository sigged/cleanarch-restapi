using Mde.WishList.Api.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Mde.WishList.Api.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
