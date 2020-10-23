using System.Text.Json.Serialization;

namespace YTMDesktop.YtmdaRest.Model
{
    public class Player
    {
        [JsonPropertyName("hasSong")] public bool HasSong { get; set; }
        [JsonPropertyName("isPaused")] public bool IsPaused { get; set; }
        [JsonPropertyName("volumePercent")] public double VolumePercent { get; set; }

        [JsonPropertyName("seekbarCurrentPosition")]
        public double SeekbarCurrentPosition { get; set; }

        [JsonPropertyName("seekbarCurrentPositionHuman")]
        public string SeekbarCurrentPositionHuman { get; set; }

        [JsonPropertyName("statePercent")] public double StatePercent { get; set; }
        [JsonPropertyName("likeStatus")] public string LikeStatus { get; set; }
        [JsonPropertyName("repeatType")] public string RepeatType { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(HasSong)}: {HasSong.ToString()}, " +
                $"{nameof(IsPaused)}: {IsPaused.ToString()}, " +
                $"{nameof(VolumePercent)}: {VolumePercent.ToString()}, " +
                $"{nameof(SeekbarCurrentPosition)}: {SeekbarCurrentPosition.ToString()}, " +
                $"{nameof(SeekbarCurrentPositionHuman)}: {SeekbarCurrentPositionHuman}, " +
                $"{nameof(StatePercent)}: {StatePercent.ToString()}, " +
                $"{nameof(LikeStatus)}: {LikeStatus}, " +
                $"{nameof(RepeatType)}: {RepeatType}";
        }
    }
}