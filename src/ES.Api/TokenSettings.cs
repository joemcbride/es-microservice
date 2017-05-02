using System;

namespace ES.Api
{
    public class TokenSettings
    {
        public string Issuer { get; set; } = "oneexchange.com";
        public string Audience { get; set; } = "oneexchange.com";
        public string Thumbprint { get; set; } = "";
        public string CertFileName { get; set; } = "server.pfx";
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);
    }
}
