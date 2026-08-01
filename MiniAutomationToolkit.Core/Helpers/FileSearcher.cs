using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiniAutomationToolkit.Core.Helpers;

public static class FileSearcher
{
    public static string FindFirstScreenshot(List<string> fileNames) // метод поиска первого .png файла в списке файлов
    {
        var screenshots = fileNames.Where(name =>
            name.EndsWith(".png", StringComparison.OrdinalIgnoreCase));

        if (!screenshots.Any()) // экспешн на случай, если такого файла нет
        {
            throw new FileNotFoundException("No screenshots found in the provided list.");
        }

        return screenshots.FirstOrDefault()!;
    }
}

