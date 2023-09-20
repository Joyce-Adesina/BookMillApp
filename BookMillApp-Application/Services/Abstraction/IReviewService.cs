using BookMillApp_Domain.Dtos.RequestDto;
using BookMillApp_Domain.Dtos.ResponseDto;
using PaperFineryApp_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Application.Services.Abstraction
{
    public interface IReviewService
    {
        Task<string> CreateReview(string userEmail, ReviewCreationDto reviewCreationDto);
        Task<StandardResponse<ReviewResponseDto>> GetReviewByUserName(string userName);
        Task<StandardResponse<IEnumerable<ReviewResponseDto>>> GetAllReviews();
    }
}
