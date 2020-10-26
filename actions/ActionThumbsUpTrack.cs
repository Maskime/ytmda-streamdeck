using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreamDeckLib;
using StreamDeckLib.Messages;
using YTMDesktop.YtmdaRest;
using YTMDesktop.YtmdaRest.Model;

namespace YTMDesktop.actions
{
    [ActionUuid(Uuid = "com.maxoumask.ytmda.action.thumbs.up")]
    public class ActionThumbsUpTrack : BaseYtmdaAction
    {
        private new static readonly ILogger Logger = Program.LoggerFactory.CreateLogger(nameof(ActionThumbsUpTrack));

        private readonly int _outlinedState = 0;
        private readonly int _filledState = 1;
        
        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            await base.OnKeyUp(args);
            YtmdaRestClient.Instance.ThumbsUpTrack();
        }

        public override async Task OnPlayerUpdate(Query query)
        {
            Logger.LogTrace($"Like status [{query.Player.LikeStatus}]");
            var isLiked = query.Player.LikeStatus?.Equals("LIKE");
            var state = _outlinedState;
            if (isLiked.HasValue)
            {
                state = isLiked.Value ? _filledState : _outlinedState;
            }

            await Manager.SetStateAsync(LastContext, state);
        }
    }
}