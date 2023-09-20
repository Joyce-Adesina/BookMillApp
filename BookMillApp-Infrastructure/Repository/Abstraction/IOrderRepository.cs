using BookMillApp_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Infrastructure.Repository.Abstraction
{
    public interface IOrderRepository : ICommandIRepository<Order>
    {
        Task<Order> GetOrderByIdAsync(string id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}
