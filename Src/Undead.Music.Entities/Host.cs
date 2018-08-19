using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Undead.Music.Entities
{
    [Table("Host")]
    public class Host
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]  
        public string Name { get; set; }
    
        public virtual Playlist Playlist { get; set; }

        public PlaylistSong GetNextSong(PlaylistSong currentSong)
        {
            return Playlist.PlaylistSongs.FirstOrDefault(s => s.Position == currentSong.Position + 1);
        }

        public PlaylistSong GetSong(int playlistSongId)
        {
            return Playlist.PlaylistSongs.FirstOrDefault(s => s.Id == playlistSongId);
        }
    }
}