using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreamDeckLib;
using StreamDeckLib.Messages;
using YTMDesktop.settings;

namespace YTMDesktop.actions
{
    public abstract class BaseYtmdaAction: BaseStreamDeckActionWithSettingsModel<Settings>
    {
        private new static readonly ILogger Logger = Program.LoggerFactory.CreateLogger(nameof(BaseYtmdaAction));

        protected new void SetModelProperties(StreamDeckEventPayload args)
        {
            base.SetModelProperties(args);
            YtmdaSettings.Instance.UpdateConfig(SettingsModel);
        }
        
        public override Task OnDidReceiveGlobalSettings(StreamDeckEventPayload args)
        {
            SetModelProperties(args);
            Logger.LogDebug($"OnDidReceiveGlobalSettings [{SettingsModel}]");
            return Task.CompletedTask;
        }
        
        public override Task OnDidReceiveSettings(StreamDeckEventPayload args)
        {
            SetModelProperties(args);
            Logger.LogDebug($"OnDidReceiveSettings [{SettingsModel}]");
            return Task.CompletedTask;
        }

        public override Task OnWillAppear(StreamDeckEventPayload args)
        {
            SetModelProperties(args);
            Logger.LogDebug($"OnWillAppear [{SettingsModel}]");
            return Task.CompletedTask;
        }
    }
}