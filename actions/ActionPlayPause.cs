using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreamDeckLib;
using StreamDeckLib.Messages;
using YTMDesktop.YtmdaRest;

namespace YTMDesktop.actions
{
    [ActionUuid(Uuid = "com.maxoumask.ytmda.action.play")]
    public class ActionPlayPause : BaseYtmdaAction
    {

        private const int PlayState = 0;
        private const int PauseState = 1;
        
        public override async Task OnWillAppear(StreamDeckEventPayload args)
        {
            await base.OnWillAppear(args);
            var playerStatus = YtmdaRestClient.Instance.PlayerStatus();
            if (playerStatus == null)
            {
                await Manager.ShowAlertAsync(args.context);
                return;
            }

            var desiredState = playerStatus.IsPaused ? PlayState : PauseState;
            var currentState = args.payload.state;
            if (desiredState != currentState)
            {
                await Manager.SetStateAsync(args.context, desiredState);
            }
        }

        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            Logger.LogDebug("OnKeyUp");
            
            await base.OnKeyUp(args);
            if (!YtmdaRestClient.Instance.TogglePlay())
            {
                await Manager.SetStateAsync(args.context, PlayState);
            }
        }
    }
}