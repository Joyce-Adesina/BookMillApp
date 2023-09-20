using AutoMapper;
using BookMillApp_Application.Services.Abstraction;
using BookMillApp_Domain.Dtos.RequestDto;
using BookMillApp_Domain.Dtos.ResponseDto;
using BookMillApp_Domain.Model;
using BookMillApp_Infrastructure.UnitOfWork.Abstraction;
using Microsoft.Extensions.Logging;
using PaperFineryApp_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Application.Services.Implementation
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SupplierService> _logger;
        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SupplierService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StandardResponse<string>> DeleteManufacturer(string id)
        {
            throw new NotImplementedException();
        }
        public async Task<StandardResponse<IEnumerable<SupplierResponseDto>>> GetAllSupplier()
        {
            var suppliers = await _unitOfWork.SupplierRepository.GetAllSuppliersAsync();
            if (suppliers == null)
            {
                return new StandardResponse<IEnumerable<SupplierResponseDto>> { Succeeded = false, Message = "null, no supplier datas found" };
            }
            var suppliersToReturn = _mapper.Map<IEnumerable<SupplierResponseDto>>(suppliers);
            return new StandardResponse<IEnumerable<SupplierResponseDto>> { Succeeded = true, Message = "Successfully retrieved suppliers", Data = suppliersToReturn };
        }
        public async Task<StandardResponse<Supplier>> GetSupplierId(string supplierId)
        {
            var getsupplier = await _unitOfWork.SupplierRepository.GetSupplierByIdAsync(supplierId);
            if (getsupplier == null)
            {
                return new StandardResponse<Supplier> { Succeeded = false, Message = "Supplier with this id: not found on record", Data = null };
            }
            return new StandardResponse<Supplier> { Succeeded = true, Message = "Supplier successfully retrieved", Data = getsupplier };
        }
        public async Task<StandardResponse<string>> UpdateSupplierr(string supplierId, UpdateSupplierDto updatSupplier)
        {
            var getsupplier = await _unitOfWork.SupplierRepository.GetSupplierByIdAsync(supplierId);
            if (getsupplier == null)
            {
                return new StandardResponse<string> { Succeeded = false, Message = "supplier with this id: not found on record" };
            }

            getsupplier.FirstName = !string.IsNullOrEmpty(updatSupplier.FirstName) ? updatSupplier.FirstName : getsupplier.FirstName;
            getsupplier.LastName = !string.IsNullOrEmpty(updatSupplier.LastName) ? updatSupplier.LastName : getsupplier.LastName;
            getsupplier.BusinessName = !string.IsNullOrEmpty(updatSupplier.BusinessName) ? updatSupplier.BusinessName : getsupplier.BusinessName;
            getsupplier.Address = !string.IsNullOrEmpty(updatSupplier.Address) ? updatSupplier.Address : getsupplier.Address;
            getsupplier.Profileimage = !string.IsNullOrEmpty(updatSupplier.Profileimage) ? updatSupplier.Profileimage : getsupplier.Profileimage;
            getsupplier.AccountName = !string.IsNullOrEmpty(updatSupplier.AccountName) ? updatSupplier.AccountName : getsupplier.AccountName;
            getsupplier.AccountNumber = !string.IsNullOrEmpty(updatSupplier.AccountNumber) ? updatSupplier.AccountNumber : getsupplier.AccountNumber;
            getsupplier.BankName = !string.IsNullOrEmpty(updatSupplier.BankName) ? updatSupplier.BankName : getsupplier.BankName;
            getsupplier.ModifiedAt = DateTime.UtcNow;
            _unitOfWork.SupplierRepository.Update(getsupplier);
            await _unitOfWork.SaveChangesAsync();
            return new StandardResponse<string> { Succeeded = true, Message = "supplier Successfully updated" };
        }
    }
}
