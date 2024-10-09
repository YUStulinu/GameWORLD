using System.ComponentModel.DataAnnotations;

namespace GameWORLD.Models
{
    public class PublishingCompany
    {
        [Key]
        public int Id { get; set; }
        public string? NameCompany { get; set; }
        public string? Address { get; set; }
        public string? ContactEmail { get; set; }
        public virtual ICollection<Game>? Game { get; set; }
    }
}