using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Logging;

namespace HdProduction.App.Common.Logging
{
  public static class Log4NetExtensions
  {
    public static ILoggingBuilder AddLog4Net(this ILoggingBuilder loggingBuilder, string logConfigFile = "log.config")
    {
      XmlConfigurator.ConfigureAndWatch(LogManager.GetRepository(Assembly.GetEntryAssembly()), new FileInfo(logConfigFile));
      return loggingBuilder.AddProvider(new Log4NetLoggerProvider());
    }
  }
}