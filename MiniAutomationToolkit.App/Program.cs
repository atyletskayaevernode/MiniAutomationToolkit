using MiniAutomationToolkit.Core.Models;
using MiniAutomationToolkit.Core.Services;
using static MiniAutomationToolkit.Core.Models.ClientType;

Console.WriteLine("MiniAutomationToolkit started");
Console.WriteLine("===");

Console.WriteLine("Task 2 test (discount calculation)");

var taskTwoExamples = new (ClientType Client, decimal Amount)[]
{
    (Vip, 500m), //exp: 75
    (Vip, 2000m), //exp: 300
    (Premium, 800m), //exp: 40
    (Premium, 1000m), //exp: 50
    (Premium, 1500m), //exp: 150
    (Regular, 500m), //exp: 0
    (Regular, 1500m), // exp: 75
    (Regular, 1000m), //exp: 0
};
foreach (var (client, amount) in taskTwoExamples)
{
    decimal discount = DiscountCalculator.CalculateDiscount(amount, client);
    Console.WriteLine($"Client: {client}, amount: {amount}, discount: {discount}");
}
Console.WriteLine("===");