using System;
using Undead.Music.Entities;

namespace Undead.Music.Data
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<Host> HostRepository { get; }

        GenericRepository<Playlist> PlaylistRepository { get; }

        GenericRepository<PlaylistSong> PlaylistSongRepository { get; }

        GenericRepository<Song> SongRepository { get; }

        GenericRepository<User> UserRepository { get; }

        void Save();
    }
}