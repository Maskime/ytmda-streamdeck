using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Logging;
using StreamDeckLib;
using StreamDeckLib.Config;
using YTMDesktop.YtmdaRest;
using YTMDesktop.YtmdaRest.Model;

namespace YTMDesktop
{
    class Program
    {
        public static ILoggerFactory LoggerFactory;
        
        private static readonly List<Func<Query, Task>> Observers = new List<Func<Query, Task>>();
        
        static async Task Main(string[] args)
        {
            using (var config = ConfigurationBuilder.BuildDefaultConfiguration(args))
            {
                LoggerFactory = config.LoggerFactory;
                var timer = new Timer
                {
                    AutoReset = true,
                    Enabled = true,
                    Interval = 1000
                };
                timer.Elapsed += UpdateObservers;
                await ConnectionManager.Initialize(args, config.LoggerFactory)
                    .RegisterAllActions(typeof(Program).Assembly)
                    .StartAsync();
            }
        }

        private static void UpdateObservers(object sender, ElapsedEventArgs e)
        {
            var query = YtmdaRestClient.Instance.Query();
            foreach (var observer in Observers)
            {
                observer(query);
            }
        }

        public static void RegisterObserver(Func<Query, Task> observer)
        {
            if (Observers.Contains(observer))
            {
                return;
            }
            Observers.Add(observer);
        }
    }
}