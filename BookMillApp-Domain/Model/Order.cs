using BookMillApp_Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMillApp_Domain.Model
{
    public class Order : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? OrderReference { get; set; }
        public TotalWeightInkg? TotalWeightInKg { get; set; }
        public string TotalWeightInkgDesc { get; set; }
        public PaperTypes? Papertype { get; set; }
        public string? PapertypeDesc { get; set; }
        public DateTime OrderInitializationTime { get; set; }
        public DeliveryModes DeliveryModes { get; set; }
        public string DeliveryModesDesc { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string OrderStatusDesc { get; set; }
        public string? SupplierLocation { get; set; }
        public string? productImageUrl { get; set; }

        [ForeignKey(nameof(Supplier))]
        public string SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
