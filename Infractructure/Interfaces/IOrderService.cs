using Domain.ApiResponse;
using Domain.DTOs.OrderDTO;

namespace Infractructure.Interfaces;

public interface IOrderService
{
    Task<PagedResponse<List<GetOrderDTO>>> GetAllOrdersAsync(int pageNumber, int pageSize);
    Task<Response<GetOrderDTO>> GetOrderByIdAsync(int id);
    Task<Response<string>> CreateOrderAsync(CreateOrderDTO createOrderDTO);
    Task<Response<string>> UpdateOrderAsync(UpdateOrderDTO updateOrderDTO);
    Task<Response<string>> DeleteOrderAsync(int id);
}
