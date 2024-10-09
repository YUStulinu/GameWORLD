using System.ComponentModel.DataAnnotations;

namespace GameWORLD.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public virtual ICollection<Rating>? Rating { get; set; }
    }
}