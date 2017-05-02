using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Alba;
using ES.Api;
using Jose;
using Shouldly;
using Xunit;

namespace ES.Api.Tests
{
    public class BasicTests
    {
        private Task<IScenarioResult> run(Action<Scenario> configuration)
        {
            using (var system = SystemUnderTest.ForStartup<Startup>(s =>
            {
                s.EnvironmentName = "Development";
            }))
            {
                return system.Scenario(configuration);
            }
        }

        [Fact]
        public async Task public_values()
        {
            var result = await run(_ =>
            {
                _.Get.Url("/api/publicvalues");
                _.Context.Request.Method.ShouldBe("GET");
            });

            result.Context.Response.StatusCode.ShouldBe(200);

            var output = result.ResponseBody.ReadAsJson<string[]>();
            output.ShouldNotBeNull();
            output.ShouldBe(new []{ "value1", "value2"});
        }

        [Fact]
        public async Task authenticated_values()
        {
            var result = await run(_ =>
            {
                var token = createToken();
                _.Context.Request.SetBearerToken(token);
                _.Get.Url("/api/values");
                _.Context.Request.Method.ShouldBe("GET");
            });

            result.Context.Response.StatusCode.ShouldBe(200);

            var output = result.ResponseBody.ReadAsJson<string[]>();
            output.ShouldNotBeNull();
            output.ShouldBe(new []{ "value1", "value2"});
        }

        private static string createToken()
        {
            var now = DateTimeOffset.UtcNow;

            var claims = new Dictionary<string, object>
            {
                {"exp", now.AddMinutes(10).ToUnixTimeSeconds()},
                {"iat", now.ToUnixTimeSeconds().ToString() },
                {"jti", Guid.NewGuid().ToString() }
            };

            var cert = new X509Certificate2("server.pfx");

            var token = JWT.Encode(claims, cert.GetRSAPrivateKey(), JwsAlgorithm.RS256);
            return token;
        }
    }
}
