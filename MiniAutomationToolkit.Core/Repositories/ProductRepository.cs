using MiniAutomationToolkit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Repositories

{
    public static class ProductRepository
    {
        public static List<Product> LoadFromCsv(string filePath) //загрузка товаров из csv файла
        {
            var products = new List<Product>();
            var lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                int lineNumber = i + 1;
                string rawLine = lines[i];

                if (string.IsNullOrWhiteSpace(rawLine))
                {
                    continue;
                }

                if (i == 0) //скип заголовков
                {
                    continue;
                }
                var parts = rawLine.Split(';');

                if (parts.Length != 3) //если делится не на 3 части, то ошибка
                {
                    throw new InvalidDataException(
                        $"Invalid product data at line {lineNumber}.");
                }

                string name = parts[0].Trim(); //обозначаем, какая часть какой переменной является и режем пробелы по краям
                string priceText = parts[1].Trim();
                string categoryText = parts[2].Trim();

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(priceText) || string.IsNullOrWhiteSpace(categoryText)) //если какая-то часть оказалась пустой после разбивки и обрезки
                {
                    throw new InvalidDataException(
                        $"Invalid product data at line {lineNumber}.");
                }

                if (!decimal.TryParse(priceText, out decimal price) || price < 0)
                {
                    throw new InvalidDataException(
                        $"Invalid product price at line {lineNumber}.");
                }

                if (!Enum.TryParse<ProductCategory>(categoryText, ignoreCase: true, out var category))
                {
                    throw new InvalidDataException(
                        $"Invalid product category at line {lineNumber}.");
                }

                products.Add(new Product(name, price, category));
            }

            return products;
        }

        //возвращает список имен товаров с ценой ниже maxPrice и категорией category, отсортированных по цене и имени
        public static List<string> GetAffordableProducts( 
            IEnumerable<Product> products,
            ProductCategory category,
            decimal maxPrice)
        {
            return products
                .Where(p => p.Category == category)
                .Where(p => p.Price < maxPrice)
                .OrderBy(p => p.Price)
                .ThenBy(p => p.Name)
                .Select(p => p.Name)
                .ToList();
        }
    }
}