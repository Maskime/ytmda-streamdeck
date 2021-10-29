using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using YTMDesktop.errors;
using YTMDesktop.settings;
using YTMDesktop.YtmdaRest.Model;
using YTMDesktop.YtmdaRest.Model.Commands;

namespace YTMDesktop.YtmdaRest
{
    public sealed class YtmdaRestClient
    {
        
        private static readonly Lazy<YtmdaRestClient> Lazy = new Lazy<YtmdaRestClient>(() => new YtmdaRestClient());
        private readonly ILogger _logger;

        public static YtmdaRestClient Instance => Lazy.Value;
        
        private YtmdaRestClient()
        {
            _logger = Program.LoggerFactory.CreateLogger(nameof(YtmdaRestClient));
        }

        public Query Query()
        {
            var settings = YtmdaSettings.Instance.GetSettings();
            return GetRequest(settings, "query", out Query result) ? result : null;
        }

        private bool GetRequest<T>(Settings config, string resourceName, out T result)
        {
            var apiUrl = $"http://{config.Host}:{config.Port.ToString()}/";
            using (var client = new HttpClient())
            {
                try
                {
                    var requestUri = $"{apiUrl}{resourceName}";
                    _logger.LogTrace($"Request URI : [{requestUri}]");
                    var streamTask = client.GetStreamAsync(requestUri).Result;
                    result = JsonSerializer.DeserializeAsync<T>(streamTask).Result;
                    _logger.LogTrace("Result obtained");
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error when requesting");
                    result = default;
                    return false;
                }
            }
        }

        public Player PlayerStatus()
        {
            var settings = YtmdaSettings.Instance.GetSettings();
            if (!GetRequest(settings, "query/player", out Player result))
            {
                return null;
            }

            _logger.LogDebug($"Received [{result}]");
            return result;

        }

        public bool TogglePlay()
        {
            var status = PlayerStatus();
            if (status == null)
            {
                throw new YtmdaRestClientException("PlayerStatus was null");
            }
            if (status.IsPaused)
            {
                _logger.LogDebug("Player is paused, sending the play-track command");
                SendCommand(new CommandTrackPlay());
            }
            else
            {
                _logger.LogDebug("Player is playing, sending the pause-track command");
                SendCommand(new CommandTrackPause());
            }
            //Check after the action the player status.
            //Basically, is the player playing.
            _logger.LogDebug("Command sent, retrieving new player status");
            var togglePlay = !PlayerStatus().IsPaused;
            _logger.LogDebug($"Is player playing [{togglePlay.ToString()}]");
            return togglePlay;
        }

        private void SendCommand(BaseCommand command)
        {
            var config = YtmdaSettings.Instance.GetSettings();
            var apiUrl = $"http://{config.Host}:{config.Port.ToString()}/";
            using (var client = new HttpClient())
            {
                try
                {
                    var commandString = JsonSerializer.Serialize(command);
                    _logger.LogDebug($"Sending [{commandString}]");
                    var content = new StringContent(commandString, Encoding.UTF8, "application/json");
                    if (!string.IsNullOrEmpty(config.Password))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.Password);
                    }
                    var response = client.PostAsync($"{apiUrl}query", content).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError(response.ReasonPhrase);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error when requesting");
                }
            }
        }

        public void NextTrack()
        {
            SendCommand(new CommandTrackNext());
        }

        public void PreviousTrack()
        {
            SendCommand(new CommandTrackPrevious());
        }

        public void ThumbsUpTrack()
        {
            SendCommand(new CommandTrackThumbsUp());
        }

        public void ThumbsDownTrack()
        {
            SendCommand(new CommandTrackThumbsDown());
        }

        public void VolumeDown()
        {
            SendCommand(new CommandVolumeDown());
        }

        public void VolumeUp()
        {
            SendCommand(new CommandVolumeUp());
        }

        public int ToggleMute(int previousVolume)
        {
            var player = PlayerStatus();
            if (player.VolumePercent > 0)
            {
                _logger.LogDebug("Muting the player");
                SendCommand(new CommandSetVolume(0));
                return player.VolumePercent;
            }
            _logger.LogDebug($"Un muting, setting volume to [{previousVolume}]");
            SendCommand(new CommandSetVolume(previousVolume));
            return previousVolume;
        }
    }
}