using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Domain.Dtos.RequestDto
{
    public class UpdateManufacturerDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Profileimage { get; set; }
        public string? BusinessName { get; set; }
        public string? PricePerKg { get; set; }
        public string? MinKilogramAccepted { get; set; }
    }
}
