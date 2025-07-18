using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs.OrderDTO;
using Domain.Entities;
using Infractructure.Interfaces;

namespace Infractructure.Services;

public class OrderService(
        IOrderRepository orderRepository,
        IRedisCacheService redisCacheService,
        IMapper mapper) : IOrderService
{
    public async Task<Response<string>> CreateOrderAsync(CreateOrderDTO createOrderDTO)
    {
        var order = mapper.Map<Order>(createOrderDTO);
        var result = await orderRepository.CreateAsync(order);

        if (result == 0)
        {
            return new Response<string>("Something went wrong", HttpStatusCode.InternalServerError);
        }
        await redisCacheService.RemoveData("orders");
        return new Response<string>("Created Order Successfully");
    }

    public async Task<Response<string>> DeleteOrderAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return new Response<string>("Order not found", HttpStatusCode.NotFound);
        }

        var result = await orderRepository.DeleteAsync(order);

        if (result == 0)
        {
            return new Response<string>("Something went wrong", HttpStatusCode.InternalServerError);
        }
        await redisCacheService.RemoveData("orders");
        return new Response<string>("Deleted Order Successfully");
    }

    public async Task<PagedResponse<List<GetOrderDTO>>> GetAllOrdersAsync(int pageNumber, int pageSize)
    {
        var pagedResult = await orderRepository.GetAllAsync(pageNumber, pageSize);

        var mapped = mapper.Map<List<GetOrderDTO>>(pagedResult.Data);

        return new PagedResponse<List<GetOrderDTO>>(mapped, pagedResult.TotalRecords, pagedResult.PageNumber, pagedResult.PageSize);
    }

    public async Task<Response<GetOrderDTO>> GetOrderByIdAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return new Response<GetOrderDTO>("Order not found", HttpStatusCode.NotFound);
        }

        var mapped = mapper.Map<GetOrderDTO>(order);
        return new Response<GetOrderDTO>(mapped);
    }

    public async Task<Response<string>> UpdateOrderAsync(UpdateOrderDTO updateOrderDTO)
    {
        var order = await orderRepository.GetByIdAsync(updateOrderDTO.Id);
        if (order == null)
        {
            return new Response<string>("Order not found", HttpStatusCode.NotFound);
        }

        mapper.Map(updateOrderDTO, order);
        var result = await orderRepository.UpdateAsync(order);

        if (result == 0)
        {
            return new Response<string>("Something went wrong", HttpStatusCode.InternalServerError);
        }
        await redisCacheService.RemoveData("orders");
        return new Response<string>("Updated Order Successfuly");
    }
}
