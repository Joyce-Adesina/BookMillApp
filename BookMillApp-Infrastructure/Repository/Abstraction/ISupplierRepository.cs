using BookMillApp_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Infrastructure.Repository.Abstraction
{
    public interface ISupplierRepository : ICommandIRepository<Supplier>
    {

        Task<Supplier> GetSupplierByIdAsync(string id);

        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
    }
}
