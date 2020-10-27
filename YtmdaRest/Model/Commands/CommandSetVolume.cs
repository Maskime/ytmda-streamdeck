namespace YTMDesktop.YtmdaRest.Model.Commands
{
    public class CommandSetVolume:BaseCommand
    {
        public CommandSetVolume(int volumePercent):base("player-set-volume", volumePercent.ToString()){}
    }
}