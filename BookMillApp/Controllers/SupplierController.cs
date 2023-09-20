using BookMillApp_Application.Services.Abstraction;
using BookMillApp_Domain.Dtos.RequestDto;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMillApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [SwaggerOperation(Description = "The endpoint retrieves all suppliers from record")]
        [HttpGet("get-allsuppliers")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var result = await _supplierService.GetAllSupplier();
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [SwaggerOperation(Description = "Thie endpoint retrieves all suppliers from record")]
        [HttpGet("get-supplierById")]
        public async Task<IActionResult> GetASupplierById(string supplierId)
        {
            var result = await _supplierService.GetSupplierId(supplierId);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [SwaggerOperation(Description = "The endpoint updates the supplier profile")]
        [HttpPatch("update-supplier")]
        public async Task<IActionResult> UpdateSupplierProfile(string supplierId, UpdateSupplierDto updatesupplier)
        {
            var result = await _supplierService.UpdateSupplierr(supplierId, updatesupplier);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
