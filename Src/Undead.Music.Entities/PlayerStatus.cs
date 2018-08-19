namespace Undead.Music.Entities
{
    public class PlayerStatus
    {
        public PlaylistSong CurrentPlaylistSong { get; set; }

        public double Volume { get; set; }

        public bool IsPlaying { get; set; }

        public bool IsShuffleEnabled{ get; set; }
    }
}