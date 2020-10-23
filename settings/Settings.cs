using System.Text.Json.Serialization;

namespace YTMDesktop.settings
{
  public class Settings
  {
    private const string DefaultHost = "localhost";
    private const int DefaultPort = 9863;
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
    [JsonPropertyName("host")]
    public string Host { get; set; } = DefaultHost;
    [JsonPropertyName("port")]
    public int Port { get; set; } = DefaultPort;

    public override string ToString()
    {
      return $"{nameof(Password)}: {Password}, {nameof(Host)}: {Host}, {nameof(Port)}: {Port.ToString()}";
    }
  }
}
