using System;
using System.IO;

namespace Undead.Music.Scanner
{
    /// <summary>
    /// Allow scan a file for it's Id3 information.
    /// </summary>
    public sealed class Id3Reader
    {
        /// <summary>
        /// The unique instance on Id3Reader.
        /// </summary>
        private static Id3Reader _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// Prevents a default instance of the Id3Reader class from being created.
        /// </summary>
        private Id3Reader()
        {
        }

        /// <summary>
        /// Gets an instance of the ID3Reader class.
        /// </summary>
        public static Id3Reader Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Id3Reader();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Scan the Id3 Tag of the file on fullPath Path, load and return an Mp3FileLink object.
        /// </summary>
        /// <param name="fullPath">The path of the file to create the MusicTrack.</param>
        /// <returns>Mp3FileLink object.</returns>
        public MusicTrack GetMusicTrackFromId3(string fullPath)
        {
            try
            {
                TagLib.File file = TagLib.File.Create(fullPath);

                string title = file.Tag.Title;
                string album = file.Tag.Album;
                string[] artists = file.Tag.Artists;
                int year = (int)file.Tag.Year;
                string[] genres = file.Tag.Genres;
                string artist = string.Empty;

                foreach (string a in artists)
                {
                    if (artist != string.Empty)
                    {
                        artist += string.Format("-{0}", a);
                    }
                    else
                    {
                        artist = a;
                    }
                }

                if (title == string.Empty && album == string.Empty && artist == string.Empty)
                {
                    return null;
                }

                if (title == string.Empty)
                {
                    title = Path.GetFileNameWithoutExtension(fullPath);
                }

                string genre = string.Empty;
                foreach (string g in genres)
                {
                    if (genre != string.Empty)
                    {
                        genre += string.Format("-{0}", g);
                    }
                    else
                    {
                        genre = g;
                    }
                }

                byte[] artwork = null;
                if (file.Tag.Pictures.Length > 0 && file.Tag.Pictures[0].Data != null)
                {
                    artwork = file.Tag.Pictures[0].Data.Data;
                }

                var duration = new TimeSpan(file.Properties.Duration.Hours, file.Properties.Duration.Minutes, file.Properties.Duration.Seconds);
                
                return new MusicTrack(title, album, artist, year, genre, duration, artwork, file.Properties.AudioBitrate);
            }
            catch
            {
                return null;
            }
        }
    }
}