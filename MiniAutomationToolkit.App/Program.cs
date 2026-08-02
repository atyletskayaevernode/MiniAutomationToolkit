using MiniAutomationToolkit.Core.Models;
using MiniAutomationToolkit.Core.Services;
using System.Diagnostics;
using static MiniAutomationToolkit.Core.Models.ClientType;
using MiniAutomationToolkit.Core.Helpers;
using MiniAutomationToolkit.Core.Pages;
using MiniAutomationToolkit.Core.Configuration;
using MiniAutomationToolkit.Core.Extensions;
using MiniAutomationToolkit.Core.Simulations;

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

foreach (var page in pages) //загрузка страниц
{
    page.Load();
}

try
{
    bool hasDuplicateUrls = pages //проверка на дубликаты урлов
        .GroupBy(page => page.Url)
        .Any(group => group.Count() > 1);

    if (hasDuplicateUrls)
    {
        throw new InvalidOperationException("Duplicates found");
    }
    else 
    {
        Console.WriteLine("All page urls are unique");
    }
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("===");

Console.WriteLine("Task 6 test (AppConfig)");

var configPath = Path.Combine(AppContext.BaseDirectory, "data", "appsettings.txt"); //путь к конфиг файлу
var config = new AppConfig(configPath); //создание объекта конфигурации
string baseUrl = config.GetSetting<string>("baseUrl");
int timeout = config.GetSetting<int>("timeout");
bool headless = config.GetSetting<bool>("headless");
int retryCount = config.GetSetting<int>("retryCount");

Console.WriteLine($"baseUrl: {baseUrl}");
Console.WriteLine($"timeout: {timeout}");
Console.WriteLine($"headless: {headless}");
Console.WriteLine($"retryCount: {retryCount}");

try
{
    config.GetSetting<string>("missingKey");
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("===");

Console.WriteLine("Task 7 test (HasHttpScheme)");
string?[] urls = //тут массив строчек с урлами, включая null и аппер кейс
{
    "https://google.com",
    "http://example.org",
    "ftp://files.example.com",
    null,
    "HTTPS://SITE.EXAMPLE.COM",
};
foreach (var url in urls) //проверка каждой строки из массива новым методом HasHttpScheme() и вывод результата в консоль
{
    bool result = url.HasHttpScheme();
    string display = url ?? "<null>";
    Console.WriteLine($"{display} → {result}");
}
Console.WriteLine("===");

Console.WriteLine("Task 8 test (LongOperations)");

var simulator = new LongOperationSimulator();

var syncStopwatch = Stopwatch.StartNew(); //запуск синхронной операции и замер времени, вывод результата и времени в консоль
string syncResult = simulator.LongOperation();
syncStopwatch.Stop();
Console.WriteLine($"Sync result: {syncResult}");
Console.WriteLine($"Sync timer: {syncStopwatch.ElapsedMilliseconds} ms");

var asyncStopwatch = Stopwatch.StartNew(); //запуск асинхронной операции и замер времени, вывод результата и времени в консоль
string asyncResult = await simulator.LongOperationAsync();
asyncStopwatch.Stop();
Console.WriteLine($"Async result: {asyncResult}");
Console.WriteLine($"Async timer: {asyncStopwatch.ElapsedMilliseconds} ms");

Console.WriteLine("===");

Console.WriteLine("Task 9 test (ErrorLogger)");

var errorLogger = new ErrorLogger();

string inputPath = Path.Combine(AppContext.BaseDirectory, "data", "input.txt"); //генерация путей к файлам
string missingPath = Path.Combine(AppContext.BaseDirectory, "data", "missing.txt");
string logPath = Path.Combine(AppContext.BaseDirectory, "data", "errors.log");

string? fileContent = errorLogger.TryReadFile(inputPath, logPath); //чтение существующего файла, запись ошибки в лог и вывод результата в консоль
Console.WriteLine("Existing file read result:");
Console.WriteLine(fileContent);

string? missingContent = errorLogger.TryReadFile(missingPath, logPath); //чтение несуществующего файла, запись ошибки в лог и вывод результата в консоль
Console.WriteLine($"Missing file read result: {(missingContent is null ? "file not found" : missingContent)}");

Console.WriteLine("Error log read result:"); //чтение лог файла и вывод результата в консоль
Console.WriteLine(File.ReadAllText(logPath));
Console.WriteLine("===");