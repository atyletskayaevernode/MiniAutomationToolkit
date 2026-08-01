using MiniAutomationToolkit.Core.Models;
using MiniAutomationToolkit.Core.Services;
using System.Diagnostics;
using static MiniAutomationToolkit.Core.Models.ClientType;
using MiniAutomationToolkit.Core.Helpers;
using MiniAutomationToolkit.Core.Pages;

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

Console.WriteLine("Task 4 test (UserDto)");

//создание 2 юзеров с одинаковыми данными, проверка на равенство
var user1 = new UserDto("John", "johndoe@example.com");
Console.WriteLine($"Created user: {user1.Name}, {user1.Email}");

var user2 = new UserDto("John", "johndoe@example.com");
Console.WriteLine($"user1 == user2: {user1 == user2}");

//я хз, как сделать так, чтобы при попытке изменить имя или почту выдавало эксепшен, т.к. record без сеттера не позволяет менять свойства и программа не билдится
//try
//{
//    user1.Name = "Notjohn";
//}
//
//try
//{
//    user1.Email = "notjohndoe@example.com";
//}

try
{
    var invalidUser = new UserDto("", "johndoe@example.com"); // пустое имя
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}

try
{
    var invalidUser = new UserDto("John", ""); // пустая почта
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}

try
{
    var invalidUser = new UserDto("John", "johndoeexample.com"); // без @
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}

try
{
    var invalidUser = new UserDto("John", "johndoe @example.com"); // пробел в почте
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine("===");

Console.WriteLine("Task 5 test (pages)");

var pages = new List<BasePage>
{
    new LoginPage(),
    new HomePage(),
};

foreach (var page in pages) 
{
    page.Load();
}

try
{
    bool hasDuplicateUrls = pages
        .GroupBy(page => page.Url)
        .Any(group => group.Count() > 1);

    if (hasDuplicateUrls)
    {
        throw new InvalidOperationException("Duplicates found");
    }

    Console.WriteLine("All page URLs are unique");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("===");