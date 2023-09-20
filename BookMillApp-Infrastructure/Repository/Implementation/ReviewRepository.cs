using BookMillApp_Domain.Model;
using BookMillApp_Infrastructure.Persistence;
using BookMillApp_Infrastructure.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Infrastructure.Repository.Implementation
{
    public class ReviewRepository : CommandRepository<Review>, IReviewRepository
    {
        private readonly DbSet<Review> reviews;
        public ReviewRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            reviews = appDbContext.Reviews;
        }

        /// <summary>
        /// Checks for reviews where the username passed in the parameter
        /// is same as the username in the AppUser property in the Review
        /// </summary>
        /// <param name="userName"></param>
        /// <return name="Review"> review made by app user</returns>

        //Returns  all reviews
        public IQueryable<Review> GetAllReviews() => reviews;

        public IQueryable<Review> GetReviewByUserName(string userName)
        {
            var review = reviews.Where(x=>x.Supplier.User.UserName == userName);
            return review;
        }
    }
}
