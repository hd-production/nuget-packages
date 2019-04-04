using Microsoft.AspNetCore.Authentication;

namespace HdProduction.App.Common.Auth
{
  public class JwtOptions : AuthenticationSchemeOptions
  {
    public string PublicKeyPath { get; set; }
    public bool IgnoreExpiration { get; set; }
  }
}