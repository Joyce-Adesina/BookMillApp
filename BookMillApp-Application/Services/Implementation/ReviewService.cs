using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookMillApp_Application.Services.Abstraction;
using BookMillApp_Domain.Dtos.RequestDto;
using BookMillApp_Domain.Dtos.ResponseDto;
using BookMillApp_Domain.Model;
using BookMillApp_Infrastructure.Repository.Abstraction;
using BookMillApp_Infrastructure.UnitOfWork.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaperFineryApp_Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Application.Services.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public ReviewService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        /// <summary>
        /// Creates a review
        /// </summary>
        /// <param name="userEmail"> Email of the app user giving review</param>
        /// <param name="comment"> The review given by the app user</param>
        /// <returns> a string message of acknowledgement for review.</returns>
        public async Task<string> CreateReview( string userEmail, ReviewCreationDto reviewCreationDto)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user != null)
            {
                var mapReview = _mapper.Map<Review>(reviewCreationDto);
                mapReview.SupplierId = user.Id;
                await _unitOfWork.ReviewRepository.CreateAsync(mapReview);
                await _unitOfWork.SaveChangesAsync();

                return "Thanks for your review";
            }
            return "Please sign in to review";
        }

        public async Task<StandardResponse<ReviewResponseDto>> GetReviewByUserName(string userName)
        {
            var review = _unitOfWork.ReviewRepository.GetReviewByUserName(userName);
            var mapReview = await review.ProjectTo<ReviewResponseDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            if (review != null)
            {
                return StandardResponse<ReviewResponseDto>.Success("success", mapReview, 200);
            }

            throw new Exception($"user with username {userName} not found");
        }

        public async Task<StandardResponse<IEnumerable<ReviewResponseDto>>> GetAllReviews()
        {
            var reviews =  _unitOfWork.ReviewRepository.GetAllReviews();
            var mapReviews =await  reviews.ProjectTo<ReviewResponseDto>(_mapper.ConfigurationProvider).ToListAsync();
            return StandardResponse<IEnumerable<ReviewResponseDto>>.Success("success", mapReviews, 200);
        }

    }
}
