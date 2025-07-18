using Domain.Enums;

namespace Domain.DTOs.OrderDTO;

public class CreateOrderDTO
{
    public OrderType OrderType { get; set; } = OrderType.Standard;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? CustomerName { get; set; }
    public DateTime DeliveryDeadline { get; set; }
    public bool IsDiscountApplied { get; set; }
}
