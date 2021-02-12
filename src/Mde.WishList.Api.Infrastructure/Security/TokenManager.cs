﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Infrastructure.Security
{
    public interface ITokenManager
    {
        string GenerateRandomToken(int size = 32);

        //Task<bool> IsValidRefreshToken(string refreshToken);

        //Task RevokeToken(string refreshToken);

        //Task<AuthenticationTokenPair> GenerateAuthTokens(IUser user);

        ///// <summary>
        ///// Returns the principal from a token, returns null if the token is invalid or expired
        ///// </summary>
        ///// <param name="accessToken"></param>
        ///// <param name="ignoreLifetime"></param>
        ///// <returns></returns>
        //Task<IPrincipal> GetAccessTokenPrincipal(string accessToken, bool ignoreExpiration = false);
    }

    public class TokenManager //: ITokenManager
    {
        //private readonly IIdentityService identityService;
        //private readonly IDeviceIdGenerator deviceIdGenerator;
        private readonly TokenSettings tokenSettings;
        //private readonly CoreContext context;
        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly ITokenValidationParametersFactory tokenValidationParmsFactory;

        public TokenManager(
            //IIdentityService identityService,
            //IDeviceIdGenerator deviceIdGenerator,
            TokenSettings tokenSettings,
            ITokenValidationParametersFactory tokenValidationParmsFactory
            //CoreContext context
        )
        {
            //this.identityService = identityService;
            //this.deviceIdGenerator = deviceIdGenerator;
            this.tokenSettings = tokenSettings;
            this.tokenValidationParmsFactory = tokenValidationParmsFactory;
            //this.context = context;

            tokenHandler = new JwtSecurityTokenHandler();
        }


        public string GenerateRandomToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Base64UrlEncoder.Encode(randomNumber);
            }
        }


        //public async Task<IPrincipal> GetAccessTokenPrincipal(string accessToken, bool ignoreExpiration = false)
        //{
        //    var tokenValidationParms = tokenValidationParmsFactory.GetDefaultValidationParameters();
        //    tokenValidationParms.ValidateLifetime = !ignoreExpiration;
        //    var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParms, out var validatedToken);
        //    return await Task.FromResult(principal);
        //}

        //public async Task<bool> IsValidRefreshToken(string refreshToken)
        //{
        //    //var user = await _userManager.FindByNameAsync(username);
        //    return await context.Set<RefreshToken>().AnyAsync(e =>
        //        e.Token == refreshToken &&
        //        e.Expires > DateTime.UtcNow //expire date is in future
        //    );
        //}

        //public async Task RevokeToken(string refreshToken)
        //{
        //    var token = await context.Set<RefreshToken>().FirstOrDefaultAsync(e =>
        //           e.Token == refreshToken
        //       );
        //    context.Set<RefreshToken>().Remove(token);
        //    await context.SaveChangesAsync();
        //}

        //public async Task<AuthenticationTokenPair> GenerateAuthTokens(IUser user)
        //{
        //    if (user == null)
        //        throw new ArgumentNullException(nameof(user));

        //    var claims = await identityService.GetClaims(user);

        //    JwtSecurityToken accessToken;
        //    RefreshToken refreshToken;

        //    //generate new refresh token
        //    refreshToken = await CreateRefreshToken(user, tokenSettings);
        //    //generate new access token
        //    accessToken = CreateAccessToken(claims, tokenSettings);

        //    return new AuthenticationTokenPair
        //    {
        //        AccessToken = tokenHandler.WriteToken(accessToken),
        //        RefreshToken = refreshToken.Token
        //    };
        //}

        //private async Task PurgeExpiredTokens()
        //{
        //    var expiredRefreshTokens = await context.Set<RefreshToken>()
        //        .Where(e =>
        //            e.Expires > DateTime.UtcNow //expire date is in future
        //        ).ToListAsync();

        //    context.Set<RefreshToken>().RemoveRange(expiredRefreshTokens);
        //    await context.SaveChangesAsync();
        //}

        //private async Task<RefreshToken> CreateRefreshToken(
        //    //ClaimsIdentity identity,
        //    IUser user,
        //    TokenSettings tokenSettings)
        //{
        //    if (user == null)
        //        throw new ArgumentNullException(nameof(user));

        //    var deviceHash = deviceIdGenerator.GetDeviceId();

        //    //remove any expired tokens
        //    await PurgeExpiredTokens();

        //    string token = GenerateRandomToken();
        //    DateTime expiration = DateTime.UtcNow.AddMinutes(tokenSettings.RefreshTokenExpiryMinutes);

        //    var refreshToken = new RefreshToken(token, expiration, user.Id, deviceHash);
        //    context.Set<RefreshToken>().Add(refreshToken);
        //    await context.SaveChangesAsync();

        //    return refreshToken;
        //}

        //private JwtSecurityToken CreateAccessToken(IEnumerable<Claim> claims, TokenSettings tokenSettings)
        //{
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.ApiJwtKey));
        //    var signatureCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    return new JwtSecurityToken(
        //        claims: claims,
        //        issuer: tokenSettings.JwtIssuer,
        //        audience: tokenSettings.JwtAudience,
        //        expires: DateTime.UtcNow.AddMinutes(tokenSettings.JwtExpiryMinutes),
        //        signingCredentials: signatureCredentials);
        //}

    }

}
