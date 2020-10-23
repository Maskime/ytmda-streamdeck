using System.Text.Json.Serialization;

namespace YTMDesktop.YtmdaRest.Model.Commands
{
    public abstract class BaseCommand
    {
        [JsonPropertyName("command")] public string Command { get; set; }
        [JsonPropertyName("value")] public string Value { get; set; }

        protected BaseCommand(string command, string value)
        {
            Command = command;
            Value = value;
        }

        protected BaseCommand(string command)
        {
            Command = command;
        }
    }
}