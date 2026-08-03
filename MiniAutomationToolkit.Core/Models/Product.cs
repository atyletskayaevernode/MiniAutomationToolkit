namespace MiniAutomationToolkit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

public record Product(string Name, decimal Price, ProductCategory Category);