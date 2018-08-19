using System;

namespace Undead.Music.Scanner
{
    /// <summary>
    /// Represents a link to a file, with some other information collected from the ID3 Tag. 
    /// </summary>
    public sealed class MusicTrack
    {
        /// <summary>
        /// Initializes a new instance of the MusicTrack class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="album">The album.</param>
        /// <param name="artist">The artist.</param>
        /// <param name="year">The year 0 (for nothing).</param>
        /// <param name="genre">The genre.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="artwork">Artwork.</param>
        public MusicTrack(string title, string album, string artist, int year, string genre, TimeSpan duration, byte[] artwork, int bitrate)
        {
            Title = title;
            Album = album;
            Artist = artist;
            Year = year;
            Genre = genre;
            Duration = duration;
            Artwork = artwork;
            Bitrate = bitrate;
        }

        /// <summary>
        /// Gets or sets the title of the MusicTrack.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the album title of the MusicTrack.
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// Gets or sets the artist name of the MusicTrack.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the year of the MusicTrack.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the genre of the MusicTrack.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the duration of the MusicTrack.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the artwork of the MusicTrack.
        /// </summary>
        public byte[] Artwork { get; set; }

        /// <summary>
        /// Gets or sets the bitrate of the MusicTrack.
        /// </summary>
        public int Bitrate { get; set; }
    }
}