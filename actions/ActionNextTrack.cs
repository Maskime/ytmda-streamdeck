using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreamDeckLib;
using StreamDeckLib.Messages;
using YTMDesktop.YtmdaRest;

namespace YTMDesktop.actions
{
    [ActionUuid(Uuid = "com.maxoumask.ytmda.action.next")]
    public class ActionNextTrack : BaseYtmdaAction
    {
        private new static readonly ILogger Logger = Program.LoggerFactory.CreateLogger(nameof(ActionNextTrack));

        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            await base.OnKeyUp(args);
            YtmdaRestClient.Instance.NextTrack();
        }
    }
}