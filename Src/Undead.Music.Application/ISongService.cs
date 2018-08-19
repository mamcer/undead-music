using Undead.Music.Entities;

namespace Undead.Music.Application
{
    public interface ISongService
    {
        bool SongExistsByHash(string fileHash);

        void AddSong(Song song);
    }
}