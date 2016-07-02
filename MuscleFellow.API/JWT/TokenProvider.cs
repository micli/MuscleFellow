using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MuscleFellow.API.JWT
{
    public class TokenEntity
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
    public class TokenProvider
    {
        private readonly TokenProviderOptions _options;
        public TokenProvider(TokenProviderOptions options)
        {
            _options = options;
        }

        public async Task<TokenEntity> GenerateToken(HttpContext context, string userName, string password)
        {
            var identity = await GetIdentity(userName);
            if (identity == null)
                return null;
            
            DateTime now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenEntity
            {
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds
            };
            return response;
        }
        private Task<ClaimsIdentity> GetIdentity(string username)
        {
            return Task.FromResult(new ClaimsIdentity(
                new System.Security.Principal.GenericIdentity(username, "Token"), 
                new Claim[] {new Claim(ClaimTypes.Name, username) }));
        }
        public static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
