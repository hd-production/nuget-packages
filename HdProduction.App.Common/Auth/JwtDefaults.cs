namespace HdProduction.App.Common.Auth
{
  public static partial class JwtDefaults
  {
    public const string AuthenticationScheme = "Bearer";
    public const string AuthenticationSchemeIgnoreExpiration = "Bearer_IgnoreExpiration";
    public const string AuthorizationHeader = "Authorization";
    public const string ClaimsRoleType = "permissions";
    public const string Issuer = "hd-production";
  }
}