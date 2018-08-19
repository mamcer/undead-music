using CrossCutting.Core.Logging;
using CrossCutting.MainModule.IOC;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Undead.Music.Application;
using Undead.Music.Entities;

namespace Undead.Music.Player
{
    public partial class MainWindow
    {
        private IHubProxy _undeadMusicHub;

        private HubConnection _hubConnection;

        private readonly IocUnityContainer _container;

        private double _muteVolume;

        private Host _host;

        private readonly PlayerStatus _playerStatus;

        private PlaylistSong NextSong { get; set; }

        private string UndeadMusicRelayUrl { get; set; }

        private string UndeadMusicPath { get; set; }

        public MainWindow()
        {
            _container = new IocUnityContainer();

            _playerStatus = new PlayerStatus();

            InitializeComponent();

            CheckConfigurationKey("HostId");

            CheckConfigurationKey("UndeadMusicRelayUrl");

            CheckConfigurationKey("UndeadMusicPath");

            var hostId = Convert.ToInt32(ConfigurationManager.AppSettings["HostId"]);
            UndeadMusicRelayUrl = ConfigurationManager.AppSettings["UndeadMusicRelayUrl"];
            UndeadMusicPath = ConfigurationManager.AppSettings["UndeadMusicPath"];

            _host = _container.Resolve<IHostService>().GetHost(hostId);
            Title = string.Format("Undead Music Player - {0}", _host.Name);
            ConsoleLog("Hostname: " + _host.Name);

            Player.LoadedBehavior = MediaState.Manual;
            Player.MediaEnded += PlayerOnMediaEnded;

            InitializeSignalR(UndeadMusicRelayUrl);
        }

        private void CheckConfigurationKey(string configurationKey)
        {
            if (ConfigurationManager.AppSettings[configurationKey] == null)
            {
                string message = string.Format("Missing {0} configuration key", configurationKey);
                _container.Resolve<ILogService>().Fatal(message);
                MessageBox.Show(message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void PlayerOnMediaEnded(object sender, RoutedEventArgs routedEventArgs)
        {
            _playerStatus.IsPlaying = false;
            if (NextSong != null)
            {
                PlayNextFile();
            }

            LookForNextSong();
        }

        private void PlayNextFile()
        {
            if (NextSong != null)
            {
                SetPlayerStatusInfo(NextSong);

                PlayFile(NextSong.Song.FileName);
            }
        }

        private async void InitializeSignalR(string url)
        {
            if (_hubConnection != null)
            {
                _hubConnection.Stop();
            }

            _hubConnection = new HubConnection(url);
            _undeadMusicHub = _hubConnection.CreateHubProxy("undeadMusicHub");

            _undeadMusicHub.On<int, int>("PlaySong", (hostId, playlistSongId) =>
            {
                try
                {
                    if (hostId == _host.Id)
                    {
                        var playlistSong = _host.GetSong(playlistSongId);
                        if (playlistSong != null)
                        {
                            SetPlayerStatusInfo(playlistSong);
                            _playerStatus.IsPlaying = true;
                            PlayFile(playlistSong.Song.FileName);
                            LookForNextSong();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _container.Resolve<ILogService>().Error(ex.Message);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _undeadMusicHub.On<int>("Play", hostId =>
            {
                try
                {
                    if (hostId == _host.Id)
                    {
                        ConsoleLog("Play");
                        Player.Dispatcher.BeginInvoke(new Action(() => Player.Play()));
                        _playerStatus.IsPlaying = true;
                        _undeadMusicHub.Invoke("PlayerStatus", _host.Id, _playerStatus);
                    }
                }
                catch (Exception ex)
                {
                    _container.Resolve<ILogService>().Error(ex.Message);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _undeadMusicHub.On<int>("Stop", hostId =>
            {
                try
                {
                    if (hostId == _host.Id)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() => Player.Stop()));
                        ConsoleLog("Stop");
                        _playerStatus.IsPlaying = false;
                        _undeadMusicHub.Invoke("PlayerStatus", _host.Id, _playerStatus);
                    }
                }
                catch (Exception ex)
                {
                    _container.Resolve<ILogService>().Error(ex.Message);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _undeadMusicHub.On<int>("Pause", hostId =>
            {
                try
                {
                    if (hostId == _host.Id)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() => Player.Pause()));
                        _playerStatus.IsPlaying = false;
                        ConsoleLog("Pause");
                        _undeadMusicHub.Invoke("PlayerStatus", _host.Id, _playerStatus);
                    }
                }
                catch (Exception ex)
                {
                    _container.Resolve<ILogService>().Error(ex.Message);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _undeadMusicHub.On<int>("VolumeUp", (hostId) =>
            {
                try
                {
                    if (hostId == _host.Id)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                var volumeValue = _playerStatus.Volume + 0.05;
                                if (volumeValue > 1.0)
                                {
                                    volumeValue = 1.0;
                                }

                                UpdateVolume(volumeValue);
                            }));
                    }
                }
                catch (Exception ex)
                {
                    _container.Resolve<ILogService>().Error(ex.Message);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _undeadMusicHub.On<int>("VolumeDown", (hostId) =>
            {
                try
                {
                    if (hostId == _host.Id)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            var volumeValue = _playerStatus.Volume - 0.05;
                            if (volumeValue < 0)
                            {
                                volumeValue = 0;
                            }

                            UpdateVolume(volumeValue);
                        }));
                    }
                }
                catch (Exception ex)
                {
                    _container.Resolve<ILogService>().Error(ex.Message);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _undeadMusicHub.On<int>("Mute", hostId =>
            {
                try
                {
                    if (hostId == _host.Id)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                ToogleMute();
                            }));
                    }
                }
                catch (Exception ex)
                {
                    _container.Resolve<ILogService>().Error(ex.Message);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _undeadMusicHub.On<int>("GetPlayerStatus", hostId =>
            {
                try
                {
                    if (hostId == _host.Id)
                    {
                        _undeadMusicHub.Invoke("PlayerStatus", hostId, _playerStatus);
                    }
                }
                catch (Exception ex)
                {
                    _container.Resolve<ILogService>().Error(ex.Message);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });


            _undeadMusicHub.On<int>("Shuffle", hostId =>
            {
                try
                {
                    if (hostId == _host.Id)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            _playerStatus.IsShuffleEnabled = !_playerStatus.IsShuffleEnabled;
                            ConsoleLog(string.Format("Shuffle:{0}", _playerStatus.IsShuffleEnabled));
                            LookForNextSong();
                        }));
                    }
                }
                catch (Exception ex)
                {
                    _container.Resolve<ILogService>().Error(ex.Message);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _undeadMusicHub.On<int>("NextSong", hostId =>
            {
                try
                {
                    if (hostId == _host.Id)
                    {
                        Player.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            PlayNextFile();
                            LookForNextSong();
                        }));
                    }
                }
                catch (Exception ex)
                {
                    _container.Resolve<ILogService>().Error(ex.Message);
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            try
            {
                await _hubConnection.Start();
            }
            catch
            {
                ConsoleLog("SignalR connection could not be established");
                throw;
            }

            ConsoleLog("SignalR connection established");
        }

        private void ToogleMute()
        {
            if (Player.Volume < 0.1)
            {
                UpdateVolume(_muteVolume);
                ConsoleLog("Unmute");
            }
            else
            {
                _muteVolume = Player.Volume;
                UpdateVolume(0);
                ConsoleLog("Mute");
            }
        }

        private void SetPlayerStatusInfo(PlaylistSong song)
        {
            _playerStatus.IsPlaying = true;
            _playerStatus.CurrentPlaylistSong = song;
            Player.Dispatcher.BeginInvoke(
                new Action(
                    () =>
                    {
                        _playerStatus.Volume = Player.Volume;
                    }
                    ));
        }

        private void PlayFile(string fileName)
        {
            Player.Dispatcher.BeginInvoke(new Action(() =>
            {
                Uri uri = new Uri(Path.Combine(UndeadMusicPath, fileName), UriKind.Absolute);
                Player.Source = uri;
                Player.Stop();
                Player.Play();
                _playerStatus.IsPlaying = true;
                lblSongName.Content = string.Format("{0} - {1} - {2}", _playerStatus.CurrentPlaylistSong.Song.Artist, _playerStatus.CurrentPlaylistSong.Song.Album, _playerStatus.CurrentPlaylistSong.Song.Title);
                ConsoleLog("Playing " + fileName);
            }));

            _undeadMusicHub.Invoke("PlayerStatus", _host.Id, _playerStatus);

            _playerStatus.IsPlaying = true;
        }

        private void ConsoleLog(string message)
        {
            Player.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                txtConsole.Text += DateTime.Now.ToString("yyyy.dd.MM-hh:mm:ss") + " - " + message + Environment.NewLine;
                                txtConsole.ScrollToEnd();
                            }));
        }

        private void LookForNextSong()
        {
            var song = _host.GetNextSong(_playerStatus.CurrentPlaylistSong);
            NextSong = song;
            if (NextSong != null)
            {
                var message = string.Format("Next song file name found: {0}, Next song id: {1}", NextSong.Song.FileName, NextSong.Song.Id);
                _container.Resolve<ILogService>().Info(message);
                ConsoleLog(message);

                if (!_playerStatus.IsPlaying)
                {
                    message = "Play Next file since there is no playing in progress";
                    _container.Resolve<ILogService>().Info(message);
                    ConsoleLog(message);
                    PlayNextFile();
                    NextSong = null;
                }
            }
            else
            {
                var msg = "There are no other songs in the playlist";
                ConsoleLog(msg);
                _container.Resolve<ILogService>().Info(msg);
            }
        }

        private void btnPlay_click(object sender, RoutedEventArgs e)
        {
            Player.Play();
            _playerStatus.IsPlaying = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            Player.Stop();
            _playerStatus.IsPlaying = false;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            Player.Pause();
        }

        private void UpdateVolume(double value)
        {
            Player.Volume = value;
            _playerStatus.Volume = Player.Volume;
            var volumePercentageValue = Player.Volume * 100;
            lblVolume.Content = string.Format("{0:N00}%", volumePercentageValue);
            ConsoleLog(string.Format("Set Volume to {0:N00}%", volumePercentageValue));
            volumeSlider.Value = Player.Volume;
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateVolume(volumeSlider.Value);
        }

        private void lblReconnect_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ConsoleLog("reconnecting...");
            InitializeSignalR(UndeadMusicRelayUrl);
        }

        private void btnToogleMute_Click(object sender, RoutedEventArgs e)
        {
            ToogleMute();
        }
    }
}