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
    public interface IManufacturerService
    {
        Task<StandardResponse<IEnumerable<ManufacturerResponseDto>>> GetAllManufacturers();
        Task<StandardResponse<string>> UpdateManufacturer(string manufacturerId, UpdateManufacturerDto updatManufacturer);
        Task<StandardResponse<Manufacturer>> GetManufacturerById(string manufacturerId);
    }
}
