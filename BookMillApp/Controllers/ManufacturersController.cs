using BookMillApp_Application.Services.Abstraction;
using BookMillApp_Domain.Dtos.RequestDto;
using BookMillApp_Domain.Dtos.ResponseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaperFineryApp_Shared;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMillApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;
        public ManufacturersController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }
        /// <summary>
        /// update manufacturers profile using manufacturerId 
        /// </summary>
        /// <param name="manufacturerId"></param>
        /// <param name="updatemanu"></param>
        /// <returns></returns>

        [HttpPatch]
        [SwaggerResponse(200, Type = typeof(StandardResponse<string>))]
        [SwaggerResponse(400, Type = typeof(ErrorResponseDto))]
        [SwaggerResponse(401, Type = typeof(ErrorResponseDto))]
        [SwaggerResponse(403, Type = typeof(ErrorResponseDto))]
        [Authorize]
        [SwaggerOperation(Description = "This endpoint updates manufacturers details")]
        [HttpPatch("update-manufacturerprofile")]
        public async Task<IActionResult> UpdateManufacturerProfile(string manufacturerId, UpdateManufacturerDto updatemanu)
        {
            var result = await _manufacturerService.UpdateManufacturer(manufacturerId, updatemanu);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        

        /// <summary>
        /// retrieve all manufacturers profile
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Description = "This endpoint retrieves all manufacturers details on record")]
        [HttpGet("get-allManufacturers")]
        public async Task<IActionResult> GetAllManufacturers()
        {
            var result = await _manufacturerService.GetAllManufacturers();
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(result);
        }


        /// <summary>
        /// get all manufacturer using manufacturerId
        /// </summary>
        /// <param name="manufacturerId"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "This endpoint retrieves all manufacturer details by id")]
        [HttpGet("get-byid")]
        public async Task<IActionResult> GetAllManufacturbyId(string manufacturerId)
        {
            var result = await _manufacturerService.GetManufacturerById(manufacturerId);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
