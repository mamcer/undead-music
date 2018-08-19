using CrossCutting.Core.Logging;
using System.Collections.Generic;
using Undead.Music.Data;
using Undead.Music.Entities;

namespace Undead.Music.Application
{
    public class HostService : IHostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;

        public HostService(IUnitOfWork unitOfWork, ILogService logService)
        {
            _unitOfWork = unitOfWork;
            _logService = logService;
        }

        public Host GetHost(int id)
        {
            return _unitOfWork.HostRepository.Get(id);
        }

        public IEnumerable<Host> GetHosts()
        {
            return _unitOfWork.HostRepository.GetAll();
        }
    }
}