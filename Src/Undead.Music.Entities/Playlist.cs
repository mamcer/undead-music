using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Undead.Music.Entities
{
    [Table("Playlist")]
    public class Playlist
    {
        public Playlist()
        {
            PlaylistSongs = new HashSet<PlaylistSong>();
        }
    
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; }
    }
}