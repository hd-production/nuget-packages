using System;
using System.Reflection;
using log4net;
using Microsoft.Extensions.Logging;

namespace HdProduction.App.Common.Logging
{
  public class Log4NetAppStartErrorLogger : ILogger
  {
    private readonly ILog _logger;

    public Log4NetAppStartErrorLogger(string name)
    {
      _logger = LogManager.GetLogger(Assembly.GetEntryAssembly(), name);
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter = null)
    {
      if (!IsEnabled(logLevel))
        return;
      var str = formatter == null ? state.ToString() : formatter(state, exception);
      if (string.IsNullOrWhiteSpace(str) || logLevel != LogLevel.Critical)
        return;
      _logger.Fatal(str, exception);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
      return logLevel == LogLevel.Critical && _logger.IsFatalEnabled;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
      return null;
    }
  }
}