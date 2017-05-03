using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;

namespace ES.Api
{
    public static class JwtExtensions
    {
        public static void UseJwtAuthentication(this IApplicationBuilder app, ICertificateLoader loader)
        {
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = createTokenParameters(loader)
            });
        }

        private static TokenValidationParameters createTokenParameters(ICertificateLoader certLoader)
        {
            var certificate = certLoader.Load();
            var tokenValidationParameters = new TokenValidationParameters
            {
                // Token signature will be verified using a private key.
                ValidateIssuerSigningKey = true,
                RequireSignedTokens = true,
                IssuerSigningKey = new X509SecurityKey(certificate),

                // Token will only be valid if contains "example.com" for "iss" claim.
                ValidateIssuer = false,
//                ValidIssuer = "example.com",

                // Token will only be valid if contains "example.com" for "aud" claim.
                ValidateAudience = false,
//                ValidAudience = "example.com",

                // Token will only be valid if not expired yet, with clock skew.
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,

                ValidateActor = false,
            };

            return tokenValidationParameters;
        }
    }
}
