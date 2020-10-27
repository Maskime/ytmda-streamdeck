using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StreamDeckLib;
using StreamDeckLib.Messages;
using YTMDesktop.settings;
using YTMDesktop.YtmdaRest.Model;

namespace YTMDesktop.actions
{
    public abstract class BaseYtmdaAction: BaseStreamDeckActionWithSettingsModel<Settings>
    {
        private new static readonly ILogger Logger = Program.LoggerFactory.CreateLogger(nameof(BaseYtmdaAction));

        protected string LastContext { get; set; }

        protected BaseYtmdaAction()
        {
            Program.RegisterObserver(OnPlayerUpdate);
        }

        protected new void SetModelProperties(StreamDeckEventPayload args)
        {
            base.SetModelProperties(args);
            YtmdaSettings.Instance.UpdateConfig(SettingsModel);
        }
        
        public override Task OnDidReceiveGlobalSettings(StreamDeckEventPayload args)
        {
            SetModelProperties(args);
            LastContext = args.context;
            Logger.LogDebug($"OnDidReceiveGlobalSettings [{SettingsModel}]");
            return Task.CompletedTask;
        }
        
        public override Task OnDidReceiveSettings(StreamDeckEventPayload args)
        {
            SetModelProperties(args);
            LastContext = args.context;
            Logger.LogDebug($"OnDidReceiveSettings [{SettingsModel}]");
            return Task.CompletedTask;
        }

        public override Task OnWillAppear(StreamDeckEventPayload args)
        {
            SetModelProperties(args);
            LastContext = args.context;
            Logger.LogDebug($"OnWillAppear [{SettingsModel}]");
            Program.EnableObserver();
            return Task.CompletedTask;
        }

        public override async Task OnKeyDown(StreamDeckEventPayload args)
        {
            LastContext = args.context;
            await base.OnKeyDown(args);
        }

        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            LastContext = args.context;
            await base.OnKeyUp(args);
        }

        public override async Task OnWillDisappear(StreamDeckEventPayload args)
        {
            LastContext = args.context;
            await base.OnWillDisappear(args);
        }

        public override async Task OnTitleParametersDidChange(StreamDeckEventPayload args)
        {
            LastContext = args.context;
            await base.OnTitleParametersDidChange(args);
        }

        public override async Task OnDeviceDidConnect(StreamDeckEventPayload args)
        {
            LastContext = args.context;
            await base.OnDeviceDidConnect(args);
        }

        public override async Task OnDeviceDidDisconnect(StreamDeckEventPayload args)
        {
            LastContext = args.context;
            await base.OnDeviceDidDisconnect(args);
        }

        public override async Task OnApplicationDidLaunch(StreamDeckEventPayload args)
        {
            LastContext = args.context;
            await base.OnApplicationDidLaunch(args);
        }

        public override async Task OnApplicationDidTerminate(StreamDeckEventPayload args)
        {
            LastContext = args.context;
            await base.OnApplicationDidTerminate(args);
        }

        public override async Task OnPropertyInspectorDidDisappear(StreamDeckEventPayload args)
        {
            LastContext = args.context;
            await base.OnPropertyInspectorDidDisappear(args);
        }

        public override async Task OnPropertyInspectorDidAppear(StreamDeckEventPayload args)
        {
            LastContext = args.context;
            await base.OnPropertyInspectorDidAppear(args);
        }

        public virtual Task OnPlayerUpdate(Query query)
        {
            return Task.CompletedTask;
        }
    }
}