using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Services
{
    public class ErrorLogger
    {
        public string? TryReadFile(string sourceFilePath, string logFilePath) //метод для нахождения и чтения лог файла
        {
            try
            {
                return File.ReadAllText(sourceFilePath);
            }
            catch (FileNotFoundException ex)
            {
                WriteErrorLog(logFilePath, ex);
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                WriteErrorLog(logFilePath, ex);
                return null;
            }
        }

        private static void WriteErrorLog(string logFilePath, Exception ex) //метод для записи в лог
        {
            string line = $"[{DateTime.Now}] {ex.GetType().Name}: {ex.Message}{Environment.NewLine}";
            File.AppendAllText(logFilePath, line);
        }
    }
}
