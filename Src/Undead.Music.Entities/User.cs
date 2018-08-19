using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Undead.Music.Entities
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string NickName { get; set; }
    }
}