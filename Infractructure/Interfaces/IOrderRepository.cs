using Domain.ApiResponse;
using Domain.Entities;

namespace Infractructure.Interfaces;

public interface IOrderRepository
{
    Task<PagedResponse<List<Order>>> GetAllAsync(int pageNumber, int pageSize);
    Task<Order?> GetByIdAsync(int id);
    Task<int> CreateAsync(Order order);
    Task<int> UpdateAsync(Order order);
    Task<int> DeleteAsync(Order order);
}
