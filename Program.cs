using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreamDeckLib;
using StreamDeckLib.Config;

namespace YTMDesktop
{
    class Program
    {
        public static ILoggerFactory LoggerFactory;
        
        static async Task Main(string[] args)
        {
            using (var config = ConfigurationBuilder.BuildDefaultConfiguration(args))
            {
                LoggerFactory = config.LoggerFactory;
                await ConnectionManager.Initialize(args, config.LoggerFactory)
                    .RegisterAllActions(typeof(Program).Assembly)
                    .StartAsync();
            }
        }
    }
}