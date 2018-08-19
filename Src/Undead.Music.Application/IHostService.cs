using System.Collections.Generic;
using Undead.Music.Entities;

namespace Undead.Music.Application
{
    public interface IHostService
    {
        IEnumerable<Host> GetHosts();

        Host GetHost(int id);
    }
}