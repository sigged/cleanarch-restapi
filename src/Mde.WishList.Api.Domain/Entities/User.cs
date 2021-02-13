using Mde.WishList.Api.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Domain.Entities
{
    public interface IUser
    {
        string Id { get; set; }
        string Email { get; set; }
        string UserName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }

    public class User : AuditableEntity, IUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
