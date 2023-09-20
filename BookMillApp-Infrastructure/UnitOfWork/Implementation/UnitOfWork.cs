using BookMillApp_Infrastructure.Persistence;
using BookMillApp_Infrastructure.Repository.Abstraction;
using BookMillApp_Infrastructure.Repository.Implementation;
using BookMillApp_Infrastructure.UnitOfWork.Abstraction;

namespace PaperFineryApp_Infrastructure.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _Context;
        private IManufacturerRepository _manufacturerRepository;
        private ISupplierRepository _supplierRepository;
        private IOrderRepository _orderRepository;
        private IReviewRepository _reviewRepository;

        public UnitOfWork(AppDbContext context)
        {
            _Context = context;
        }
        public IManufacturerRepository ManufacturerRepository => _manufacturerRepository ??= new ManufacturerRepository(_Context);
        public ISupplierRepository SupplierRepository => _supplierRepository ??= new SupplierRepository(_Context);
        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_Context);
        public IReviewRepository ReviewRepository => _reviewRepository ??= new ReviewRepository(_Context);
        public async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync();
        }
    }
}