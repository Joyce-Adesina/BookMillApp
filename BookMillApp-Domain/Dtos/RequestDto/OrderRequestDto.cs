using BookMillApp_Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Domain.Dtos.RequestDto
{
    public class OrderRequestDto
    {
        public TotalWeightInkg TotalWeightInKg { get; set; }
        public PaperTypes PaperTypes { get; set; }
        public DeliveryModes DeliveryMode { get; set; }
        public string? SupplierLocation { get; set; }
        public IEnumerable<IFormFile> ProductImages { get; set; }
    }
}
