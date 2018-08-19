using System.Data.Entity;
using Undead.Music.Entities;

namespace Undead.Music.Data
{
    public interface IUndeadMusicEntities : IEntities
    {
        DbSet<Host> Hosts { get; set; }

        DbSet<Playlist> Playlists { get; set; }

        DbSet<PlaylistSong> PlaylistSongs { get; set; }

        DbSet<Song> Songs { get; set; }
        
        DbSet<User> Users { get; set; }

        EntityState GetState(object entity);

        void SetModified(object entity);

        DbSet<T> GetSet<T>() where T : class;
    }
}