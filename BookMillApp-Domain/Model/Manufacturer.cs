using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMillApp_Domain.Model
{
    public class Manufacturer : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Profileimage { get; set; }
        public string? BusinessName { get; set; }
        [Column(TypeName = "Money")]
        public decimal? PricePerKg { get; set; }
        public double? MinKilogramAccepted { get; set; }
        public ICollection<Review> Reviews { get; set; }

        [Key]
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
