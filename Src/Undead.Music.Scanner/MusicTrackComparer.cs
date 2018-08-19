using System;
using System.Collections.Generic;

namespace Undead.Music.Scanner
{
    public sealed class MusicTrackComparer
    {
        private static MusicTrackComparer _instance;

        private MusicTrackComparer()
        {         
        }

        public static MusicTrackComparer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MusicTrackComparer();
                }

                return _instance;
            }
        }

        private int Levenshtein(string a, string b)
        {

            if (string.IsNullOrEmpty(a))
            {
                if (!string.IsNullOrEmpty(b))
                {
                    return b.Length;
                }
                return 0;
            }

            if (string.IsNullOrEmpty(b))
            {
                if (!string.IsNullOrEmpty(a))
                {
                    return a.Length;
                }
                return 0;
            }

            int cost;
            int[,] d = new int[a.Length + 1, b.Length + 1];
            int min1;
            int min2;
            int min3;

            for (int i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }

            for (Int32 i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }

            for (int i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (int j = 1; j <= d.GetUpperBound(1); j += 1)
                {
                    cost = Convert.ToInt32(!(a[i - 1] == b[j - 1]));

                    min1 = d[i - 1, j] + 1;
                    min2 = d[i, j - 1] + 1;
                    min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];

        }

        private string RemoveSpecialCharacters(string text)
        {
            string specialCharacters = "áéíóúñÁÉÍÓÚÑ";
            string replacementCharacters = "aeiounAEIOUN";
            for (int i = 0; i < specialCharacters.Length; i++)
            {
                string special = specialCharacters.Substring(i, 1);
                string replacement = replacementCharacters.Substring(i, 1);
                text = text.Replace(special, replacement);
            }

            return text;
        }

        private int SpecialCharactersCount(string text)
        {
            string specialCharacters = "áéíóúñÁÉÍÓÚÑ";
            int count = 0;
            for (int i = 0; i < specialCharacters.Length; i++)
            {
                if (text.Contains(specialCharacters[i].ToString()))
                {
                    count += 1;
                }
            }

            return count;
        }

        private string NormalizeString(string text)
        {
            return this.RemoveSpecialCharacters(text.ToLower());
        }

        public bool Compare(ref MusicTrack mt, List<MusicTrack> lmt)
        {
            string title1 = this.NormalizeString(mt.Title);
            foreach (MusicTrack musicTrack in lmt)
            {
                string title2 = this.NormalizeString(musicTrack.Title);
                if (this.Levenshtein(title1, title2) == 0)
                {
                    string artist1 = this.NormalizeString(mt.Artist);
                    string artist2 = this.NormalizeString(musicTrack.Artist);
                    string album1 = this.NormalizeString(mt.Album);
                    string album2 = this.NormalizeString(musicTrack.Album);
                    if ((artist1 == string.Empty || artist2 == string.Empty || this.Levenshtein(artist1, artist2) <= 3) && (album1 == string.Empty || album2 == string.Empty || this.Levenshtein(album1, album2) <= 3))
                    {
                        if (mt.Artist == string.Empty || this.SpecialCharactersCount(musicTrack.Artist) > this.SpecialCharactersCount(mt.Artist))
                        {
                            mt.Artist = musicTrack.Artist;
                        }

                        if (mt.Album == string.Empty || this.SpecialCharactersCount(musicTrack.Album) > this.SpecialCharactersCount(mt.Album))
                        {
                            mt.Album = musicTrack.Album;
                        }

                        if (this.SpecialCharactersCount(musicTrack.Title) > this.SpecialCharactersCount(mt.Title))
                        {
                            mt.Title = musicTrack.Title;
                        }

                        if (mt.Genre == string.Empty)
                        {
                            mt.Genre = musicTrack.Genre;
                        }

                        if (mt.Year == 0)
                        {
                            mt.Year = musicTrack.Year;
                        }

                        if (mt.Artwork == null)
                        {
                            mt.Artwork = musicTrack.Artwork;
                        }

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
