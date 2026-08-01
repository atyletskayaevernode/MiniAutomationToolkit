using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Configuration
{
    public class AppConfig
    {
        private readonly Dictionary<string, string> _settings = new(); //словарь сеттингов ключ,значение
        public AppConfig(string filePath) //конструктор, принимающий путь к файлу и вызывающий метод чтения
        {
            LoadSettings(filePath);
        }
        private void LoadSettings(string filePath) //метод чтения файла конфигурации
        {
            foreach (var rawLine in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(rawLine))
                {
                    continue;
                }
                var line = rawLine.Trim(); //удаление лишних пробелов, если есть (только по краям строки!)
                if (line.TrimStart().StartsWith("#"))
                {
                    continue;
                }
                var parts = line.Split('=', 2); //делим строку на ключ и значение по символу = (parts[0] - ключ, parts[1] - значение)
                if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]))
                {
                    throw new InvalidDataException($"Invalid configuration line: '{rawLine}'");
                }
                var key = parts[0].Trim(); //удаление лишних пробелов у ключа (которые были бы перед =)
                var value = parts[1].Trim(); //удаление лишних пробелов у значения (которые были бы после =)
                if (_settings.ContainsKey(key)) //если ключ не уникальный - кидаем эксепшн
                {
                    throw new InvalidDataException($"Duplicate configuration key: '{key}'");
                }
                _settings[key] = value; //сохранение ключа и значения в словарь
            }
        }
        public T GetSetting<T>(string key) //метод получения значения по ключу с приведением к типу T
        {
            if (!_settings.TryGetValue(key, out var value)) //если ключ не найден - кидаем эксепшн
            {
                throw new KeyNotFoundException($"Setting '{key}' was not found.");
            }
            try //пытаемся привести значение к типу T
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                throw new InvalidDataException(
                    $"Cannot convert setting '{key}' to type '{typeof(T).Name}'.");
            }
        }
    }
}