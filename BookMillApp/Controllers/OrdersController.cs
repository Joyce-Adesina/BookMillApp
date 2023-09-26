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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICloudinaryService _cloudinaryService;

        public OrdersController(IOrderService orderService, ICloudinaryService cloudinaryService)
        {
            _orderService = orderService;
            _cloudinaryService = cloudinaryService;
        }

        /// <summary>
        /// create order for record
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="OrderRequestDto"></param>
        /// <returns></returns>

        [HttpPost]
        [SwaggerResponse(200, Type = typeof(StandardResponse<string>))]
        [SwaggerResponse(400, Type = typeof(ErrorResponseDto))]
        [SwaggerResponse(401, Type = typeof(ErrorResponseDto))]
        [SwaggerResponse(403, Type = typeof(ErrorResponseDto))]

        [SwaggerOperation(Description = "This endpoint creates an order from record")]
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(string orderId, [FromForm] OrderRequestDto OrderRequestDto)
        {
            var result = await _orderService.CreateOrder(orderId, OrderRequestDto);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        /// <summary>
        /// retrieves all orders for record
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Description = "Thie endpoint retrieve all orders from record")]
        [HttpGet("get-allorders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrders();
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        /// <summary>
        /// get a single order using orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "This endpoint gets an Order from record")]
        [HttpGet("get-orderId")]
        public async Task<IActionResult> GetorderById(string orderId)
        {
            var result = await _orderService.GetOrderId(orderId);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        /// <summary>
        /// uploads file documents
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        [SwaggerOperation(Description = "This endpoint uploads documents")]
        [HttpPost("file-upload")]
        public async Task<IActionResult> FileUpload(IEnumerable<IFormFile> images)
        {
            var result = await _cloudinaryService.UploadImage(images);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
