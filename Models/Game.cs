using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameWORLD.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int SoftwareDeveloperId { get; set; }
        [ForeignKey("SoftwareDeveloperId")]
        public  SoftwareDeveloper? SoftwareDeveloper { get; set; }
        [ForeignKey("PublishingCompanyId")]
        public int PublishingCompanyId { get; set; }
        public  PublishingCompany? PublishingCompany { get; set; }
        public  ICollection<Rating>? Rating { get; set; }

        public ICollection<GameGameGenre>? GameGameGenre { get; set; }
    }
}