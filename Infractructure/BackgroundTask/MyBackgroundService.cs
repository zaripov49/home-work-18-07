using Domain.Enums;
using Infractructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infractructure.BackgroundTask;

public class MyBackgroundService(
    ILogger<MyBackgroundService> logger,
    IServiceScopeFactory serviceScopeFactory)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation($"My Background task is started: {DateTime.Now}");
            await using var scope = serviceScopeFactory.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var orders = await dbContext.Orders
            .Where(o => !o.IsDiscountApplied)
            .ToListAsync(stoppingToken);

            int discountedCount = 0;

            foreach (var order in orders)
            {
                var timeElapsed = DateTime.UtcNow - order.CreatedAt;

                var allowedTime = order.OrderType switch
                {
                    OrderType.Express => TimeSpan.FromHours(2),
                    OrderType.Standard => TimeSpan.FromDays(1),
                    OrderType.Economy => TimeSpan.FromDays(3),
                    _ => TimeSpan.MaxValue
                };

                if (timeElapsed > allowedTime)
                {
                    var oldPrice = order.Price;
                    order.Price *= 0.8m;
                    order.IsDiscountApplied = true;

                    logger.LogInformation($"Скидка 20% применена к заказу #{order.Id}: была {oldPrice}, стала {order.Price}");
                    discountedCount++;
                }
            }

            if (discountedCount > 0)
            {
                await dbContext.SaveChangesAsync(stoppingToken);
                logger.LogInformation($"Всего применено скидок: {discountedCount}");
            }
            else
            {
                logger.LogInformation($"Просроченных заказов не найдено: {DateTime.Now}");
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }

}