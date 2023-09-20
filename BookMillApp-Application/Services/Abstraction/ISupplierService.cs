using BookMillApp_Domain.Dtos.RequestDto;
using BookMillApp_Domain.Dtos.ResponseDto;
using BookMillApp_Domain.Model;
using PaperFineryApp_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Application.Services.Abstraction
{
    public interface ISupplierService
    {
        Task<StandardResponse<IEnumerable<SupplierResponseDto>>> GetAllSupplier();
        Task<StandardResponse<Supplier>> GetSupplierId(string supplierId);
        Task<StandardResponse<string>> DeleteManufacturer(string id);
        Task<StandardResponse<string>> UpdateSupplierr(string supplierId, UpdateSupplierDto updatSupplier);
    }
}
