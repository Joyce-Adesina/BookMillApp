using BookMillApp_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Infrastructure.Repository.Abstraction
{
    public interface IReviewRepository : ICommandIRepository<Review>
    {
        IQueryable<Review> GetAllReviews();
        IQueryable<Review> GetReviewByUserName(string userName);
    }
}
