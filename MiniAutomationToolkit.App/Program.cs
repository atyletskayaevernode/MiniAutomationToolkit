using MiniAutomationToolkit.Core.Models;
using MiniAutomationToolkit.Core.Services;
using System.Diagnostics;
using static MiniAutomationToolkit.Core.Models.ClientType;
using MiniAutomationToolkit.Core.Helpers;

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

Console.WriteLine("Task 3 test (FileSearcher)");

var fileNames = new List<string> //список с png файлами
{
    "screen_001.jpeg",
    "screen_002.jpg",
    "error_2024.log",
    "screen_003.png",
    "SCREEN_004.PNG",
    "debug.txt",
    "screenshot_final.PnG",
    "image.bmp",
    "photo.gif",
    "archive.zip",
    "document.pdf",
    "presentation.pptx",
    "spreadsheet.xlsx",
    "notes.txt",
    "script.js",
    "style.css",
    "index.html",
    "README.md",
    "config.json",
    "data.xml"
};

string firstScreenshot = FileSearcher.FindFirstScreenshot(fileNames); //поиск по списку 1
Console.WriteLine($"First .png file: {firstScreenshot}");

var fileNamesWithoutScreenshots = new List<string> //список без png
{
    "001.jpeg",
    "002.jpg",
    "error_2024.log",
    "003.jpg",
    "004.jpg",
    "debug.txt",
    "screenshot_final",
    "image.bmp",
    "photo.gif",
    "archive.zip",
    "document.pdf",
    "presentation.pptx",
    "spreadsheet.xlsx",
    "notes.txt",
    "script.js",
    "style.css",
    "index.html",
    "README.md",
    "config.json",
    "data.xml"
};

try //поиск по списку 2, плюс обработка эксепшена
{
    FileSearcher.FindFirstScreenshot(fileNamesWithoutScreenshots);
}
catch (FileNotFoundException ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine("===");


