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
    public class SupplierRepository : CommandRepository<Supplier>, ISupplierRepository
    {
        private readonly DbSet<Supplier> _supplier;

        public SupplierRepository(AppDbContext dbContext) : base(dbContext)
        {
            _supplier = dbContext.Set<Supplier>();
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            var suppliers = await _supplier.OrderBy(x => x.UserId).ToListAsync();
            return suppliers;
        }

        public async Task<Supplier> GetSupplierByIdAsync(string id)
        {
            return await _supplier.Where(m => m.UserId.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
