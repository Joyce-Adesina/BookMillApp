using BookMillApp_Domain.Dtos.RequestDto;
using BookMillApp_Domain.Dtos.ResponseDto;
using BookMillApp_Domain.Model;
using PaperFineryApp_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Application.Services.Abstraction
{
    public interface IOrderService
    {
        Task<StandardResponse<IEnumerable<OrderResponseDto>>> GetAllOrders();
        Task<StandardResponse<Order>> GetOrderId(string orderId);
        Task<StandardResponse<OrderResponseDto>> CreateOrder(string supplierId, OrderRequestDto createOrder);
    }
}
