using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Undead.Music.Entities
{
    [Table("PlaylistSong")]
    public class PlaylistSong
    {
        public int Id { get; set; }

        [Required]
        public int Position { get; set; }

        public User User { get; set; }

        public virtual Song Song { get; set; }
    }
}