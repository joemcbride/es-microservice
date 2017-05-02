using System.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using ES.Api;

namespace ES.Api.Identity
{
    public class CertificateLoader : ICertificateLoader
    {
        private readonly IHostingEnvironment _env;
        private readonly TokenSettings _tokenSettings;

        public CertificateLoader(IHostingEnvironment env, TokenSettings tokenSettings)
        {
            _env = env;
            _tokenSettings = tokenSettings;
        }

        public X509Certificate2 Load()
        {
            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                var certs = store.Certificates.Find(X509FindType.FindByThumbprint, _tokenSettings.Thumbprint, false);

                if (certs.Count < 1) throw new SecurityException("Unable to find certificate");
                return certs[0];
            }
        }
    }
}
