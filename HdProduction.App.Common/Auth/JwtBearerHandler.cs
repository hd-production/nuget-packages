using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace HdProduction.App.Common.Auth
{
  public class JwtAuthBearerHandler : AuthenticationHandler<JwtOptions>
  {
    private static readonly ClaimsPrincipal DebugUser = new ClaimsPrincipal(
      new ClaimsIdentity(new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.Email, "DebugUser"),
        new Claim(JwtRegisteredClaimNames.Sub, "-1")
      }, JwtDefaults.AuthenticationScheme, ClaimTypes.Email, JwtDefaults.ClaimsRoleType));

    private static JsonWebKey _signInKey;

    private TokenValidationParameters _validationParameters;

    public JwtAuthBearerHandler(IOptionsMonitor<JwtOptions> options, ILoggerFactory logger, UrlEncoder encoder,
      ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override async Task InitializeHandlerAsync()
    {
      if (_signInKey == null)
      {
        using (var stream = File.OpenText(Options.PublicKeyPath))
        {
          _signInKey = new JsonWebKey(await stream.ReadToEndAsync());
        }
      }

      _validationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        ValidIssuer = Options.ClaimsIssuer,
        ValidateAudience = false,
        ValidateLifetime = !Options.IgnoreExpiration,
        IssuerSigningKey = _signInKey,
      };
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      ClaimsPrincipal user;
      if (TryAuthorize(Context, out SecurityToken validatedToken))
      {
        user = new ClaimsPrincipal(new ClaimsIdentity(((JwtSecurityToken) validatedToken).Claims,
          JwtDefaults.AuthenticationScheme, JwtRegisteredClaimNames.Email, JwtDefaults.ClaimsRoleType));
      }
      else if (Debugger.IsAttached)
      {
        user = DebugUser;
      }
      else
      {
        return Task.FromResult(AuthenticateResult.NoResult());
      }

      return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(user, JwtDefaults.AuthenticationScheme)));
    }

    private bool TryAuthorize(HttpContext context, out SecurityToken validatedToken)
    {
      validatedToken = null;
      string jwtToken = null;
      if (context.Request.Headers.TryGetValue(JwtDefaults.AuthorizationHeader, out StringValues authHeaderValues))
      {
        string[] authHeader = authHeaderValues.ToString().Split(' ');
        jwtToken = authHeader.Length >= 2 &&
                   authHeader[0].Equals(JwtDefaults.AuthenticationScheme, StringComparison.Ordinal)
                   && !String.IsNullOrWhiteSpace(authHeader[1])
          ? authHeader[1].Trim()
          : null;
      }

      if (jwtToken == null)
      {
        return false;
      }

      try
      {
        new JwtSecurityTokenHandler().ValidateToken(jwtToken, _validationParameters, out validatedToken);
      }
      catch (SecurityTokenException)
      {
        return false;
      }
      catch (ArgumentException)
      {
        return false;
      }

      return true;
    }
  }
}