using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Mde.WishList.Api.Infrastructure.Security
{
    public interface ITokenValidationParametersFactory
    {
        TokenValidationParameters GetDefaultValidationParameters();
    }

    public class TokenValidationParametersFactory : ITokenValidationParametersFactory
    {
        private TokenSettings tokenSettings;

        public TokenValidationParametersFactory(TokenSettings settings)
        {
            tokenSettings = settings;
        }

        public TokenValidationParameters GetDefaultValidationParameters()
        {
            byte[] jwtKey = Encoding.ASCII.GetBytes(tokenSettings.ApiJwtKey);

            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = tokenSettings.JwtIssuer,
                ValidAudience = tokenSettings.JwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(jwtKey),

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
