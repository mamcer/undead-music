using System;
using Undead.Music.Entities;

namespace Undead.Music.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UndeadMusicEntities _context = new UndeadMusicEntities();
        private GenericRepository<Host> _hostRepository;
        private GenericRepository<Playlist> _playlistRepository;
        private GenericRepository<PlaylistSong> _playlistSongRepository;
        private GenericRepository<Song> _songRepository;
        private GenericRepository<User> _userRepository;
        private bool _disposed;

        public GenericRepository<Host> HostRepository
        {
            get
            {
                if (_hostRepository == null)
                {
                    _hostRepository = new GenericRepository<Host>(_context);
                }

                return _hostRepository;
            }
        }

        public GenericRepository<Playlist> PlaylistRepository
        {
            get
            {
                if (_playlistRepository == null)
                {
                    _playlistRepository = new GenericRepository<Playlist>(_context);
                }

                return _playlistRepository;
            }
        }

        public GenericRepository<PlaylistSong> PlaylistSongRepository
        {
            get
            {
                if (_playlistSongRepository == null)
                {
                    _playlistSongRepository = new GenericRepository<PlaylistSong>(_context);
                }

                return _playlistSongRepository;
            }
        }

        public GenericRepository<Song> SongRepository
        {
            get
            {
                if (_songRepository == null)
                {
                    _songRepository = new GenericRepository<Song>(_context);
                }

                return _songRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<User>(_context);
                }

                return _userRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}