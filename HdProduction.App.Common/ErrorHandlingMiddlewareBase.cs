using System;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Http;

namespace HdProduction.App.Common
{
  public abstract class ErrorHandlingMiddlewareBase
  {
    protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    private readonly RequestDelegate _next;

    protected ErrorHandlingMiddlewareBase(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next.Invoke(context);
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    protected abstract Task HandleExceptionAsync(HttpContext context, Exception ex);
  }
}