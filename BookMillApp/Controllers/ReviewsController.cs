using BookMillApp_Application.Services.Abstraction;
using BookMillApp_Domain.Dtos.RequestDto;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMillApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviews;

        public ReviewsController(IReviewService reviews)
        {
            _reviews = reviews;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReview( string userEmail, ReviewCreationDto reviewCreationDto)
        {
            //watch this
            //var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var responseFromCreation = await _reviews.CreateReview(userEmail, reviewCreationDto);
            return Ok(responseFromCreation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var result = await _reviews.GetAllReviews();
            return Ok(result);
        }

        [HttpGet("by-username")]
        public async Task<IActionResult> GetReviewsByUserName(string userName)
        {
            var review = await _reviews.GetReviewByUserName(userName);
            return Ok(review);
        }
    }
}
