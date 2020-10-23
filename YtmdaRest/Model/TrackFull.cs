using System.Text.Json.Serialization;

namespace YTMDesktop.YtmdaRest.Model
{
    public class TrackFull : TrackBasic
    {
        [JsonPropertyName("album")] public string Album { get; set; }

        [JsonPropertyName("durationHuman")] public string DurationHuman { get; set; }

        [JsonPropertyName("url")] public string Url { get; set; }

        [JsonPropertyName("id")] public string Id { get; set; }

        [JsonPropertyName("isVideo")] public bool IsVideo { get; set; }

        [JsonPropertyName("isAdvertisement")] public bool IsAdvertisement { get; set; }

        [JsonPropertyName("inLibrary")] public bool InLibrary { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(Album)}: {Album}, " +
                   $"{nameof(DurationHuman)}: {DurationHuman}, " +
                   $"{nameof(Url)}: {Url}, " +
                   $"{nameof(Id)}: {Id}, " +
                   $"{nameof(IsVideo)}: {IsVideo.ToString()}, " +
                   $"{nameof(IsAdvertisement)}: {IsAdvertisement.ToString()}, " +
                   $"{nameof(InLibrary)}: {InLibrary.ToString()}";
        }
    }
}