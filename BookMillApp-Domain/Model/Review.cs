using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMillApp_Domain.Model
{
    public class Review : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Comment { get; set; }

        [ForeignKey(nameof(Supplier))]
        public string SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
