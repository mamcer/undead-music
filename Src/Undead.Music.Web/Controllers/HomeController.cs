using System.Linq;
using System.Web.Mvc;
using Undead.Music.Application;
using Undead.Music.Web.Models;

namespace Undead.Music.Web.Controllers
{
    public class HomeController : Controller
    {
        private IHostService _hostService;

        public HomeController(IHostService hostService)
        {
            _hostService = hostService;
        }

        public ActionResult Index()
        {
            var hosts = _hostService.GetHosts();

            return View(new IndexViewData { Hosts = hosts.Select(h => new HostDto { Id = h.Id, Name = h.Name }) });
        }
    }
}