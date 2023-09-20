using BookMillApp_Domain.Model;
using BookMillApp_Infrastructure.Persistence;
using BookMillApp_Infrastructure.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace BookMillApp_Infrastructure.Repository.Implementation
{
    public class OrderRepository : CommandRepository<Order>, IOrderRepository
    {
        private readonly DbSet<Order> _order;

        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
            _order = dbContext.Set<Order>();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {  
            var order = await _order.OrderBy(x => x.Id).ToListAsync();
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(string id)
        {
            return await _order.Where(m => m.Id.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
