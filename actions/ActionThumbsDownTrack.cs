using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreamDeckLib;
using StreamDeckLib.Messages;
using YTMDesktop.YtmdaRest;
using YTMDesktop.YtmdaRest.Model;

namespace YTMDesktop.actions
{
    [ActionUuid(Uuid = "com.maxoumask.ytmda.action.thumbs.down")]
    public class ActionThumbsDownTrack : YtmdaActionBase
    {
        private new static readonly ILogger Logger = Program.LoggerFactory.CreateLogger(nameof(ActionThumbsDownTrack));

        private readonly int _outlinedState = 0;
        private readonly int _filledState = 1;
        
        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            await base.OnKeyUp(args);
            YtmdaRestClient.Instance.ThumbsDownTrack();
        }

        public override async Task OnPlayerUpdate(Query query)
        {
            Logger.LogTrace($"Like status [{query.Player.LikeStatus}]");
            var isDisliked = query.Player.LikeStatus?.Equals("DISLIKE");
            var state = _outlinedState;
            if (isDisliked.HasValue)
            {
                state = isDisliked.Value ? _filledState : _outlinedState;
            }

            await Manager.SetStateAsync(LastContext, state);
        }
    }
}