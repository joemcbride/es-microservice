using System.Security.Cryptography.X509Certificates;

namespace ES.Api
{
  public interface ICertificateLoader
  {
      X509Certificate2 Load();
  }
}