using BookMillApp_Application.Services.Abstraction;
using BookMillApp_Domain.Dtos.RequestDto;
using BookMillApp_Domain.Dtos.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PaperFineryApp_Shared;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMillApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthServices _authService;

        public AuthenticationController(IAuthServices authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers new user to the BookMill
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [SwaggerResponse(200, Type = typeof(StandardResponse<string>))]
        [SwaggerResponse(400, Type = typeof(ErrorResponseDto))]
        [SwaggerResponse(401, Type = typeof(ErrorResponseDto))]
        [SwaggerResponse(403, Type = typeof(ErrorResponseDto))]

        public async Task<IActionResult> RegisterUser([FromForm] RegisterRequestDto register)
        {
            var result = await _authService.RegisterUser(register);
            if (!result.Item1.Succeeded)
            {
                return BadRequest(result.Item1);
            }
            string encodedToken = System.Text.Encodings.Web.UrlEncoder.Default.Encode(result.token);
            string callback_url = Request.Scheme + "://" + Request.Host + $"/api/authentication/confirm-email/{register.Email}/{encodedToken}";

           _authService.SendConfirmationEmail(register.Email, callback_url);
            return Ok(StandardResponse<string>.Success("Email confirmation sent to your inbox", string.Empty, 200));
        }

        /// <summary>
        /// logins in user to BookMill
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] UserLoginRequestDto login)
        {
            var result = await _authService.Login(login);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// confirmation of email using token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>

        [HttpGet("confirm-email/{email}/{token}")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            string decodedToken = WebUtility.UrlDecode(token);
            await _authService.ConfirmEmailAddress(email, decodedToken);
            return Ok(StandardResponse<string>.Success("Email confirmed successfully", string.Empty, 200));
        }

        /// <summary>
        /// get allUsers using the app
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-allUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _authService.GetUsers();
            if (result.Count() < 1)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
