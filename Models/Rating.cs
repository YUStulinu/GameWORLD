using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameWORLD.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public string? Text { get; set; }
        public int Review { get; set; }
        public string? Title { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? Username { get; set; }
        
        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public  Game? Game { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
    }
}