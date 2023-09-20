using BookMillApp_Domain.Dtos.RequestDto;
using BookMillApp_Domain.Dtos.ResponseDto;
using BookMillApp_Domain.Model;
using PaperFineryApp_Shared;

namespace BookMillApp_Application.Services.Abstraction
{
    public interface IAuthServices
    {
        Task<(StandardResponse<RegisterResponseDto>, string token)> RegisterUser(RegisterRequestDto register);
        Task<StandardResponse<object>> Login(UserLoginRequestDto login);
        Task<IEnumerable<User>> GetUsers();
        Task<StandardResponse<string>> ConfirmEmailAddress(string email, string token);
        void SendConfirmationEmail(string email, string callback_url);
    }
}
