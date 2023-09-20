using BookMillApp_Infrastructure.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Infrastructure.UnitOfWork.Abstraction
{
    public interface IUnitOfWork
    {
        IManufacturerRepository ManufacturerRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IOrderRepository OrderRepository { get; }
        IReviewRepository ReviewRepository { get; }
        Task SaveChangesAsync();
    }
}
