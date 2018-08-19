using System.Linq;
using System.Web.Mvc;
using Undead.Music.Application;
using Undead.Music.Web.Models;

namespace Undead.Music.Web.Controllers
{
    public class HostController : Controller
    {
        private IHostService _hostService;

        public HostController(IHostService hostService)
        {
            _hostService = hostService;
        }

        public ActionResult Index(int id)
        {
            var host = _hostService.GetHost(id);
            return View(new HostViewData { Id = host.Id, HostName = host.Name, PlaylistName = host.Playlist.Name, Playlist = host.Playlist.PlaylistSongs.Select(p => new PlaylistSongDto { Id = p.Id, Title = string.Format("{0}. {1} - {2} - {3}", p.Position, p.Song.Artist, p.Song.Album, p.Song.Title) })});
        }
    }
}