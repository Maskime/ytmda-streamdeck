using System.Text.Json.Serialization;

namespace YTMDesktop.YtmdaRest.Model
{
    public class TrackBasic
    {
        [JsonPropertyName("cover")]
        public string Cover {get;set;}
        [JsonPropertyName("author")]
        public string Author {get;set;}
        [JsonPropertyName("title")]
        public string Title {get;set;}
        [JsonPropertyName("duration")]
        public int Duration {get;set;}

        public override string ToString()
        {
            return $"{nameof(Cover)}: {Cover}, " +
                   $"{nameof(Author)}: {Author}, " +
                   $"{nameof(Title)}: {Title}, " +
                   $"{nameof(Duration)}: {Duration.ToString()}";
        }
    }
}