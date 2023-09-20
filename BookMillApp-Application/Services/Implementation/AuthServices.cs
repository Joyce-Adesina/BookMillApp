using AutoMapper;
using BookMillApp_Application.Services.Abstraction;
using BookMillApp_Domain.Dtos.RequestDto;
using BookMillApp_Domain.Dtos.ResponseDto;
using BookMillApp_Domain.Enum;
using BookMillApp_Domain.Model;
using BookMillApp_Infrastructure.UnitOfWork.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PaperFineryApp_Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Application.Services.Implementation
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthServices(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<(StandardResponse<RegisterResponseDto>, string token)> RegisterUser([FromForm] RegisterRequestDto register)
        {
            var userExist = await _userManager.FindByEmailAsync(register.Email);
            var userExistByUsername = await _userManager.FindByNameAsync(register.UserName);

            if (userExist != null)
            {
                return (new StandardResponse<RegisterResponseDto>()
                {
                    Succeeded = false,
                    Message = $"User with this email address '{register.Email}' already exists. Please select another email to proceed."
                }, string.Empty);
            }
            else if (userExistByUsername != null)
            {
                return (new StandardResponse<RegisterResponseDto>()
                {
                    Succeeded = false,
                    Message = $"User with this username '{register.UserName}' already exists. Please select another username."
                },string.Empty);
            }

            User user = _mapper.Map<User>(register);
            user.UserName = register.UserName;
            user.Email = register.Email;
            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                if (register.UserType == UserType.Manufacturer)
                {
                    await _userManager.AddToRoleAsync(user, Role.Regular.ToString());
                    var findUser = await _userManager.FindByEmailAsync(register.Email);
                    var createManufacturer = new Manufacturer()
                    {
                        UserId = findUser.Id,
                        IsActive = true
                    };
                    _unitOfWork.ManufacturerRepository.CreateAsync(createManufacturer);
                    await _unitOfWork.SaveChangesAsync();
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(findUser);
                    return (new StandardResponse<RegisterResponseDto>() { Succeeded = true, Message = "User successfully registered as a Manufacturer" },token);
                }
                await _userManager.AddToRoleAsync(user, Role.Regular.ToString());
                var getUser = await _userManager.FindByEmailAsync(register.Email);
                if (getUser == null)
                {
                    return (new StandardResponse<RegisterResponseDto>() { Succeeded = false, Message = "no user found" }, string.Empty);
                }
                var createSupplier = new Supplier()
                {
                    UserId = getUser.Id,
                    IsActive = true
                };
                await _unitOfWork.SupplierRepository.CreateAsync(createSupplier);
                await _unitOfWork.SaveChangesAsync();
                string emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(getUser);

                return (new StandardResponse<RegisterResponseDto>() { Succeeded = true, Message = "User succeefully registered as a Supplier" }, emailToken);
            }
            string errors = string.Empty;
            foreach (var error in result.Errors)
            {
                if (!error.Description.Contains("Username"))
                    errors += error.Description.TrimEnd('.') + ", ";
            }
            return (new StandardResponse<RegisterResponseDto>() { Succeeded = false, Message = errors }, string.Empty);
        }

        public async Task<StandardResponse<object>> Login(UserLoginRequestDto login)
        {
            User user = await _userManager.Users.FirstOrDefaultAsync(u => (u.Email == login.Email));
            if (user == null)
            {
                return new StandardResponse<object>()
                {
                    Succeeded = false,
                    Message = $"There is no User with this Email: {login.Email} "
                };
            }
            var result = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!result)
            {
                return new StandardResponse<object>()
                {
                    Succeeded = false,
                    Message = $"Invalid Password, Please re-enter your password correctly"
                };
            }
            var claims = new[]
            {
                new Claim("email", login.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id ),
                new Claim(ClaimTypes.Role, Role.Admin.ToString())
            };

            //Encrypt the token
            var keyString = Environment.GetEnvironmentVariable("KEY");
            if (string.IsNullOrEmpty(keyString))
            {
                throw new InvalidOperationException("The 'KEY' environment variable is not set.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            return new StandardResponse<object>()
            {
                Succeeded = true,
                Message = "Logged in successful",
                Data = new {Token = tokenAsString}
            };
        }

        public void SendConfirmationEmail(string email, string callback_url)
        {
            string title = "Book Mill Confirmation";
            string body = $"<html><body><br/><br/>Please click to confirm your email address for Book Mill.<p/> <a href={callback_url}>Verify Your Email</a> <p/>Thank you for choosing Book Mill.<p/></body></html>";
            _emailService.SendEmail(email, title, body);
        }

        public async Task<StandardResponse<string>> ConfirmEmailAddress(string email, string token)
        {
            string trimedToken = token.Replace(" ", "+");
            User user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return StandardResponse<string>.Failed("User does not exist", 404);
            }
            if (user.EmailConfirmed)
            {
                return StandardResponse<string>.Failed("User email already confirmed", 400);
            }
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, trimedToken);
            if (!result.Succeeded)
            {
                return StandardResponse<string>.Failed("Confirmation failed", 400);
            }
            return StandardResponse<string>.Success("Email confirmed successfully", string.Empty, 200);
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            var getUsers = await _userManager.Users.ToListAsync();
            if (getUsers == null)
            {
                return Enumerable.Empty<User>();
            }
            return getUsers;
        }

    }
}
