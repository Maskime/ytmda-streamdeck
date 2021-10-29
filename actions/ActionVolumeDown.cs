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
    [ActionUuid(Uuid = "com.maxoumask.ytmda.action.volume.down")]
    public class ActionVolumeDown : YtmdaActionBase
    {
        private new static readonly ILogger Logger = Program.LoggerFactory.CreateLogger(nameof(ActionVolumeDown));
        private int _lastVolume;

        public override async Task OnWillAppear(StreamDeckEventPayload args)
        {
            await base.OnWillAppear(args);
            _lastVolume = YtmdaRestClient.Instance.PlayerStatus().VolumePercent;
        }

        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            await base.OnKeyUp(args);
            YtmdaRestClient.Instance.VolumeDown();
        }

        public override async Task OnPlayerUpdate(Query query)
        {
            if (query.Player.VolumePercent == _lastVolume)
            {
                await Manager.SetTitleAsync(LastContext, "");
            }
            else
            {
                await Manager.SetTitleAsync(LastContext, $"{query.Player.VolumePercent:0.##}%");
                _lastVolume = query.Player.VolumePercent;
            }
        }
    }
}