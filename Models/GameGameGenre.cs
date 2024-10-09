using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameWORLD.Models
{
    public class GameGameGenre
    {
        [Key]
        public int Id { get; set; }

        public int GameGenreId { get; set; }

        public int GameId { get; set; }

        [ForeignKey("GameId")]
        public Game? Game { get; set; }
        [ForeignKey("GameGenreId")]
        public GameGenre? GameGenre { get; set; }
    }
}
