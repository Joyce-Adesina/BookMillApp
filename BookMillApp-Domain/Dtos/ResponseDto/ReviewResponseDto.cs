using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Domain.Dtos.ResponseDto
{
    public class ReviewResponseDto
    {
        public string Id { get; set; }
        public SupplierResponseDto UserReviewer { get; set; }
        public string Comment { get; set; }
    }
}
