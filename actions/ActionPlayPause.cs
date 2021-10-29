using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreamDeckLib;
using StreamDeckLib.Messages;
using YTMDesktop.YtmdaRest;
using YTMDesktop.YtmdaRest.Model;

namespace YTMDesktop.actions
{
    [ActionUuid(Uuid = "com.maxoumask.ytmda.action.play")]
    public class ActionPlayPause : YtmdaActionBase
    {
        private new static readonly ILogger Logger = Program.LoggerFactory.CreateLogger(nameof(ActionPlayPause));

        private enum ButtonState
        {
            Play = 0,
            Pause = 1,
            LostConnection = 2
        }

        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            Logger.LogDebug("OnKeyUp");
            await base.OnKeyUp(args);
            YtmdaRestClient.Instance.TogglePlay();
        }

        public override async Task OnPlayerUpdate(Query query)
        {
            ButtonState state;
            if (query == null)
            {
                state = ButtonState.LostConnection;
            }
            else
            {
                Logger.LogTrace("Received player update");
                state = query.Player.IsPaused ? ButtonState.Play : ButtonState.Pause;
            }

            await Manager.SetStateAsync(LastContext, (int)state);
        }
    }
}