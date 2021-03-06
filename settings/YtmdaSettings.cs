﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace YTMDesktop.settings
{
    public sealed class YtmdaSettings
    {
        private static readonly Lazy<YtmdaSettings> Lazy = new Lazy<YtmdaSettings>(() => new YtmdaSettings());
        private readonly ILogger _logger;
        public static YtmdaSettings Instance => Lazy.Value;

        private const string ConfigFileName = "ytmda_settings.json";

        private static readonly object PadLock = new object();

        private static readonly string ConfigFilePath = Path
            .Combine(
                Path.GetDirectoryName(typeof(Program).Assembly.Location),
                ConfigFileName);

        private Settings _settingsCache;

        private YtmdaSettings()
        {
            _logger = Program.LoggerFactory.CreateLogger(nameof(YtmdaSettings));
        }

        private List<Settings> ReadConfigFile()
        {
            _logger.LogDebug($"Reading config file at [{ConfigFilePath}]");
            lock (PadLock)
            {
                return JsonSerializer
                        .Deserialize<SettingsCollection>(File.ReadAllText(ConfigFilePath))
                        .SettingsList
                    ;
            }
        }

        public Settings GetSettings()
        {
            if (_settingsCache != null)
            {
                return _settingsCache;
            }

            _settingsCache = ReadConfigFile()[0];
            return _settingsCache;
        }

        public void UpdateConfig(Settings settings)
        {
            _logger.LogDebug("Updating config file");
            var settingsList = ReadConfigFile();
            settingsList[0] = settings;
            UpdateConfigFile(settingsList);
            _settingsCache = settings;
        }

        private void UpdateConfigFile(List<Settings> settingsList)
        {
            lock (PadLock)
            {
                var settingsCollection = new SettingsCollection {SettingsList = settingsList};
                File.WriteAllText(ConfigFilePath, JsonSerializer.Serialize(settingsCollection));
            }
        }
    }
}