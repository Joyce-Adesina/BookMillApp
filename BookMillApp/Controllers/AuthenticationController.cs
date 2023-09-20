using BookMillApp_Application.Services.Abstraction;
using BookMillApp_Domain.Dtos.RequestDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PaperFineryApp_Shared;
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

        [HttpPost("Register")]
       
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

        [HttpGet("confirm-email/{email}/{token}")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            string decodedToken = WebUtility.UrlDecode(token);
            await _authService.ConfirmEmailAddress(email, decodedToken);
            return Ok(StandardResponse<string>.Success("Email confirmed successfully", string.Empty, 200));
        }

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
