using MiniAutomationToolkit.Core.Models;
using static MiniAutomationToolkit.Core.Models.ClientType;

namespace MiniAutomationToolkit.Core.Services;

public static class DiscountCalculator
{
    public static decimal CalculateDiscount(
        decimal orderAmount,
        ClientType clientType)
    {
        if (orderAmount < 0) //эксепшн на случай отрицательной суммы заказа
        {
            throw new ArgumentOutOfRangeException(
                nameof(orderAmount),
                "order amount can't be negative");
        }

        decimal orderDiscountRate = clientType switch //подсчет процента скидки в зависимости от типа клиента и суммы заказа
        {
            Vip => 0.15m,
            Premium => orderAmount > 1000m ? 0.10m : 0.05m,
            Regular => orderAmount > 1000m ? 0.05m : 0.00m,
            _ => throw new ArgumentOutOfRangeException(nameof(clientType))
        };

        return orderAmount * orderDiscountRate;
    }
}
