using BookMillApp_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Infrastructure.Repository.Abstraction
{
    public interface IManufacturerRepository : ICommandIRepository<Manufacturer>
    {
        Task<Manufacturer> GetManufacterByIdAsync(string id);
        Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync();
    }
}
