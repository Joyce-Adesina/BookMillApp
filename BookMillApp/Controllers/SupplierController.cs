using BookMillApp_Application.Services.Abstraction;
using BookMillApp_Domain.Dtos.RequestDto;
using BookMillApp_Domain.Dtos.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using PaperFineryApp_Shared;
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
        /// <summary>
        /// get all suppliers
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [SwaggerResponse(200, Type = typeof(StandardResponse<string>))]
        [SwaggerResponse(400, Type = typeof(ErrorResponseDto))]
        [SwaggerResponse(401, Type = typeof(ErrorResponseDto))]
        [SwaggerResponse(403, Type = typeof(ErrorResponseDto))]

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

        /// <summary>
        /// get suppliers profile using supplierId
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "This endpoint retrieves all suppliers from record")]
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
        /// <summary>
        /// update supplier's profile using supplierId
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="updatesupplier"></param>
        /// <returns></returns>
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
