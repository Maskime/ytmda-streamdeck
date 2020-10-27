using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreamDeckLib;
using StreamDeckLib.Messages;
using YTMDesktop.YtmdaRest;
using YTMDesktop.YtmdaRest.Model;

namespace YTMDesktop.actions
{
    [ActionUuid(Uuid = "com.maxoumask.ytmda.action.volume.mute")]
    public class ActionVolumeMute : BaseYtmdaAction
    {
        private new static readonly ILogger Logger = Program.LoggerFactory.CreateLogger(nameof(ActionVolumeMute));
        private int _lastVolume;
        private bool _lastVolumeSet;

        private const int MuteState = 0;
        private const int UnMuteState = 1;
        
        public override async Task OnWillAppear(StreamDeckEventPayload args)
        {
            await base.OnWillAppear(args);
            if (!_lastVolumeSet)
            {
                _lastVolume = YtmdaRestClient.Instance.PlayerStatus().VolumePercent;
                _lastVolumeSet = true;
            }
        }

        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            await base.OnKeyUp(args);
            _lastVolume = YtmdaRestClient.Instance.ToggleMute(_lastVolume);
        }

        public override async Task OnPlayerUpdate(Query query)
        {
            var state = query.Player.VolumePercent > 0 ? MuteState : UnMuteState;
            await Manager.SetStateAsync(LastContext, state);
        }
    }
}