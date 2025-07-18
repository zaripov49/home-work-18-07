using AutoMapper;
using Domain.DTOs.OrderDTO;
using Domain.Entities;

namespace Infractructure.AutoMapper;

public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<Order, GetOrderDTO>();
        CreateMap<CreateOrderDTO, Order>();
        CreateMap<UpdateOrderDTO, Order>();
    }
}
