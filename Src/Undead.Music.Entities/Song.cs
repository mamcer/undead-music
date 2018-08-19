using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Undead.Music.Entities
{
    [Table("Song")]
    public class Song
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string Album { get; set; }

        [Required]
        [MaxLength(255)]
        public string Artist { get; set; }

        public int? Year { get; set; }

        [MaxLength(255)]
        public string Genre { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        public byte[] Artwork { get; set; }

        [Required]
        public int Bitrate { get; set; }

        [Required]
        public string Hash { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }
    }
}