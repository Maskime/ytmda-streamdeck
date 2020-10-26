using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreamDeckLib;
using StreamDeckLib.Messages;
using YTMDesktop.YtmdaRest;
using YTMDesktop.YtmdaRest.Model;

namespace YTMDesktop.actions
{
    [ActionUuid(Uuid = "com.maxoumask.ytmda.action.play")]
    public class ActionPlayPause : BaseYtmdaAction
    {
        private new static readonly ILogger Logger = Program.LoggerFactory.CreateLogger(nameof(ActionPlayPause));

        private const int PlayState = 0;
        private const int PauseState = 1;

        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            Logger.LogDebug("OnKeyUp");
            await base.OnKeyUp(args);
            YtmdaRestClient.Instance.TogglePlay();
        }

        public override async Task OnPlayerUpdate(Query query)
        {
            Logger.LogTrace("Received player update");
            var state = query.Player.IsPaused ? PlayState : PauseState;

            await Manager.SetStateAsync(LastContext, state);
        }
    }
}