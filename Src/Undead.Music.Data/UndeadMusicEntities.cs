using System.Data.Entity;
using Undead.Music.Entities;

namespace Undead.Music.Data
{
    public class UndeadMusicEntities : DbContext, IUndeadMusicEntities
    {
        public UndeadMusicEntities() : base("name=UndeadMusicEntities")
        {
        }

        public virtual DbSet<Host> Hosts { get; set; }

        public virtual DbSet<Playlist> Playlists { get; set; }

        public virtual DbSet<PlaylistSong> PlaylistSongs { get; set; }

        public virtual DbSet<Song> Songs { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public EntityState GetState(object entity)
        {
            return Entry(entity).State;
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public DbSet<T> GetSet<T>() where T : class
        {
            return Set<T>();
        }
    }
}