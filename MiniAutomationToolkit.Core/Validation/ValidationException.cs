using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Validation
{
    public class ValidationException : Exception //свой эксепшн для валидации, наследуется от базового класса Exception
    {
        public ValidationException(string message)
            : base(message) { }
    }
}
