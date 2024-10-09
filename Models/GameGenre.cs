using System.ComponentModel.DataAnnotations;

namespace GameWORLD.Models
{
    public class GameGenre
    {
        [Key]
        public int Id { get; set; }
        public string? NameCategory { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<GameGameGenre>? GameGameGenre { get; set; }
    }
}

