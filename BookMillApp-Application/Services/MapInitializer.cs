using AutoMapper;
using BookMillApp_Domain.Dtos.RequestDto;
using BookMillApp_Domain.Dtos.ResponseDto;
using BookMillApp_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookMillApp_Application.Services
{
    public class MapInitializer : Profile
    {
        public MapInitializer()
        {
            CreateMap<Manufacturer, ManufacturerResponseDto>();
            CreateMap<UpdateManufacturerDto, Manufacturer>();
            CreateMap<Supplier, SupplierResponseDto>();
            CreateMap<Order, OrderResponseDto>();
            CreateMap<OrderRequestDto, Order>();
            CreateMap<User, RegisterResponseDto>();
            CreateMap<RegisterRequestDto, User>();
            CreateMap<ReviewCreationDto, Review>();
            CreateMap<Review,  ReviewResponseDto>().ForMember(m=>m.UserReviewer, opt=>opt.MapFrom(m=>m.Supplier));
        }
    }
}
