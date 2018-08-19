using System.ComponentModel.DataAnnotations;

namespace Undead.Music.Web.Models
{
    public class HostDto
    {
        public int Id { get; set; }

        [Display(Name = "Host Name")]
        public string Name { get; set; }
    }
}