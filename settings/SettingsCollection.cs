using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace YTMDesktop.settings
{
    public class SettingsCollection
    {
        [JsonPropertyName("settings")]
        public List<Settings> SettingsList { get; set; }
    }
}