using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Infrastructure.Security
{
    public class TokenSettings
    {
        public string ApiJwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public int JwtExpiryMinutes { get; set; }
        public int RefreshTokenExpiryMinutes { get; set; }
    }
}
