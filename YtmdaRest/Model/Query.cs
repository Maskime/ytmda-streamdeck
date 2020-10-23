using System.Text.Json.Serialization;

namespace YTMDesktop.YtmdaRest.Model
{
    public class Query
    {
        [JsonPropertyName("player")]
        public Player Player { get; set; }
        [JsonPropertyName("track")]
        public TrackFull Track { get; set; }

        public override string ToString()
        {
            return $"{nameof(Player)}: [{Player}], {nameof(Track)}: [{Track}]";
        }
    }
}