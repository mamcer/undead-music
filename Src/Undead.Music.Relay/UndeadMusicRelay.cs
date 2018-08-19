using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Undead.Music.Entities;

namespace Soulstone.Relay
{
    [HubName("undeadMusicHub")]
	public class UndeadMusicRelay : Hub
	{
        public void PlayFile(int hostId, int playlistSongId)
        {
            Clients.Others.PlaySong(hostId, playlistSongId);
        }

        public void Play(int hostId)
        {
            Clients.Others.Play(hostId);
        }

        public void Stop(int hostId)
        {
            Clients.Others.Stop(hostId);
        }

        public void Pause(int hostId)
        {
            Clients.Others.Pause(hostId);
        }

        public void VolumeUp(int hostId)
        {
            Clients.Others.VolumeUp(hostId);
        }

        public void VolumeDown(int hostId)
        {
            Clients.Others.VolumeDown(hostId);
        }

        public void Mute(int hostId)
        {
            Clients.Others.Mute(hostId);
        }

        public void NextSong(int hostId)
        {
            Clients.Others.NextSong(hostId);
        }

        public void PreviousSong(int hostId)
        {
            Clients.Others.PreviousSong(hostId);
        }

        public void GetPlayerStatus(int hostId)
        {
            Clients.Others.GetPlayerStatus(hostId);
        }

        public void PlayerStatus(int hostId, PlayerStatus playerStatus)
        {
            Clients.Others.PlayerStatus(hostId, playerStatus);
        }
    }
}