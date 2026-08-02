using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Validation
{
    public static class Guard //класс для метода проверки числа на положительность
    {
        public static void EnsurePositive(
        int number,
        string parameterName = "number")

        {
            if (number <= 0)
            {
                throw new ValidationException($"Validation failed: {parameterName} must be positive. Value: {number}");
            }
        }

    }
}
