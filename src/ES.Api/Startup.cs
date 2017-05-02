using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ES.Api.Identity;

namespace ES.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var apiSettings = Configuration.Get<ApiSettings>();

            services.AddSingleton<IConfigurationRoot>(Configuration);
            services.AddSingleton(Configuration.Get<TokenSettings>());
            services.AddSingleton<ICertificateLoader, CertificateLoader>();

            if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                services.AddSingleton<ICertificateLoader, MacOSCertificateLoader>();
            }

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "Origin",
                    builder => builder
                        .WithOrigins(apiSettings.AllowedOrigins)
                        .AllowAnyHeader()
                        .WithMethods("POST"));
            });

            services
              .AddAuthorization()
              .AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("Origin");

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters =
                    createTokenParameters(app.ApplicationServices.GetService<ICertificateLoader>()),
            });

            app.UseMvc();
        }

        private TokenValidationParameters createTokenParameters(ICertificateLoader certLoader)
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
