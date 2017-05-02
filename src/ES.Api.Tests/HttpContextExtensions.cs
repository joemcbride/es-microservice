using System;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace ES.Api.Tests
{
    public static class HttpRequestExtensions
    {
        public static void SetBearerToken(this HttpRequest request, string token)
        {
            request.Headers.Add("Authorization", $"Bearer {token}");
        }
    }
}
