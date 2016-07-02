using System;
using Microsoft.IdentityModel.Tokens;

namespace MuscleFellow.API.JWT
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/api/v1/Account/Login";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(500);
        public SigningCredentials SigningCredentials { get; set; }
    }
}
