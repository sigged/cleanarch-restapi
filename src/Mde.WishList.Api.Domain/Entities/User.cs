using Mde.WishList.Api.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Domain.Entities
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }

    }
}
