using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Pages
{
    public abstract class BasePage //абстрактный класс (невозможно создать объект этого класса), обязующий каждую страницу иметь урл и имя + есть метод загрузки
    {
        public abstract string Url { get; }
        public abstract string PageName { get; }

        public virtual void Load()
        {
            Console.WriteLine($"Loading page: {PageName} at {Url}");
        }
    }
}
