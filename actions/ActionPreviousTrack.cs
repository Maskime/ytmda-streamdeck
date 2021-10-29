using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreamDeckLib;
using StreamDeckLib.Messages;
using YTMDesktop.YtmdaRest;

namespace YTMDesktop.actions
{
    [ActionUuid(Uuid = "com.maxoumask.ytmda.action.previous")]
    public class ActionPreviousTrack : YtmdaActionBase
    {
        private new static readonly ILogger Logger = Program.LoggerFactory.CreateLogger(nameof(ActionNextTrack));

        public override Task OnKeyUp(StreamDeckEventPayload args)
        {
            YtmdaRestClient.Instance.PreviousTrack();
            return Task.CompletedTask;
        }
    }
}