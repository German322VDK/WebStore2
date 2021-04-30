using Microsoft.Extensions.Logging;
using System.Xml;
using System.Collections.Concurrent;

namespace WebStore.Logger
{
    public class Log4NETLoggerProvider : ILoggerProvider
    {
        private readonly string _ConfiguratioFile;
        private readonly ConcurrentDictionary<string, Log4NETLogger> _Loggers = new();

       public Log4NETLoggerProvider(string ConfiguratioFile) => _ConfiguratioFile = ConfiguratioFile;

        public ILogger CreateLogger(string Category) =>
            _Loggers.GetOrAdd(Category, category =>
            {
                var xml = new XmlDocument();
                xml.Load(_ConfiguratioFile);
                return new Log4NETLogger(category, xml["log4net"]);
            });

        public void Dispose() => _Loggers.Clear();
    }
}
