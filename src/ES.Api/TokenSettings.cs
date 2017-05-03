using System;

namespace ES.Api
{
    public class TokenSettings
    {
        public string Issuer { get; set; } = "example.com";
        public string Audience { get; set; } = "example.com";
        public string Thumbprint { get; set; }
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);
        public string CertFileName { get; set; } = "server.pfx";
        public string Mode { get; set; } = "Local";
    }
}
