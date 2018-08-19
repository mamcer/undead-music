using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Undead.Music.Web.Models
{
    public class HostViewData
    {
        public int Id { get; set; }

        [Display(Name = "Host Name")]
        public string HostName { get; set; }

        [Display(Name = "Playlist Name")]
        public string PlaylistName { get; set; }

        public IEnumerable<PlaylistSongDto> Playlist { get; set; }
    }
}