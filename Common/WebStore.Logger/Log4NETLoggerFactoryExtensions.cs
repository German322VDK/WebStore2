using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

namespace WebStore.Logger
{
    public static class Log4NETLoggerFactoryExtensions
    {
        private static string CheckFilePath(string FilePath)
        {
            if (FilePath is not { Length: > 0 }) 
                throw new ArgumentException("Некорректный путь к файлу", nameof(FilePath));

            if (Path.IsPathRooted(FilePath)) return FilePath;

            var assembly = Assembly.GetEntryAssembly();
            var dir = Path.GetDirectoryName(assembly!.Location);

            return Path.Combine(dir!, FilePath);
        }

        public static ILoggerFactory AddLog4Net(this ILoggerFactory Factory, 
            string ConfiguratioFile = "log4net.config")
        {
            Factory.AddProvider(new Log4NETLoggerProvider(CheckFilePath(ConfiguratioFile)));

            return Factory;
        }
    }
}
