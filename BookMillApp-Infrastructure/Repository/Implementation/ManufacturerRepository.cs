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
    public class ManufacturerRepository : CommandRepository<Manufacturer>, IManufacturerRepository
    {
        private readonly DbSet<Manufacturer> _manufacturer;

        public ManufacturerRepository(AppDbContext dbContext) : base(dbContext)
        {
            _manufacturer = dbContext.Set<Manufacturer>();
        }

        public async Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync()
        {
            var manufacturers = await _manufacturer.OrderByDescending(c => c.CreatedAt).ToListAsync();
            return manufacturers;
        }


        public async Task<Manufacturer> GetManufacterByIdAsync(string id)
        {
            return await _manufacturer.Where(m => m.UserId.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
