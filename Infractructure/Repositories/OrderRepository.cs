using Domain.ApiResponse;
using Domain.Entities;
using Domain.Paginations;
using Infractructure.Data;
using Infractructure.Interfaces;

namespace Infractructure.Repositories;

public class OrderRepository(DataContext context) : IOrderRepository
{
    public async Task<int> CreateAsync(Order order)
    {
        await context.Orders.AddAsync(order);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Order order)
    {
        context.Orders.Remove(order);
        return await context.SaveChangesAsync();
    }

    public async Task<PagedResponse<List<Order>>> GetAllAsync(int pageNumber, int pageSize)
{
    var query = context.Orders.AsQueryable();

    var pagination = new Pagination<Order>(query);
    return await pagination.GetPagedResponseAsync(pageNumber, pageSize);
}


    public async Task<Order?> GetByIdAsync(int id)
    {
        var order = await context.Orders.FindAsync(id);
        return order;
    }

    public async Task<int> UpdateAsync(Order order)
    {
        context.Orders.Update(order);
        return await context.SaveChangesAsync();
    }
}
