using Domain.ApiResponse;
using Domain.DTOs.OrderDTO;
using Infractructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<PagedResponse<List<GetOrderDTO>>> GetAllOrdersAsync(int pageNumber, int pageSize)
    {
        return await orderService.GetAllOrdersAsync(pageNumber, pageSize);
    }

    [HttpGet("{id:int}")]
    public async Task<Response<GetOrderDTO>> GetOrderByIdAsync(int id)
    {
        return await orderService.GetOrderByIdAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateOrderAsync(CreateOrderDTO createOrderDTO)
    {
        return await orderService.CreateOrderAsync(createOrderDTO);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateOrderAsync(UpdateOrderDTO updateOrderDTO)
    {
        return await orderService.UpdateOrderAsync(updateOrderDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteOrderAsync(int id)
    {
        return await orderService.DeleteOrderAsync(id);
    }
}
