using CrossCutting.Core.Logging;
using System.Linq;
using Undead.Music.Data;
using Undead.Music.Entities;

namespace Undead.Music.Application
{
    public class SongService : ISongService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;

        public SongService(IUnitOfWork unitOfWork, ILogService logService)
        {
            _unitOfWork = unitOfWork;
            _logService = logService;
        }

        public bool SongExistsByHash(string fileHash)
        {
            return _unitOfWork.SongRepository.Search(s => s.Hash == fileHash).Any();
        }

        public void AddSong(Song song)
        {
            _unitOfWork.SongRepository.Insert(song);
            _unitOfWork.Save();
        }
    }
}