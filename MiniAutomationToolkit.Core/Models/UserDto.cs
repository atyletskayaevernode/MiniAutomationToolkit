using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Models
{
    public record UserDto //record (я так поняла, для удобства сравнения) + свойства только для чтения, и объект неизменяемый. = читается как ==
    {
        public string Name { get; }
        public string Email { get; }

        public UserDto(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"Invalid name: <{name}>");
            }
            if (string.IsNullOrWhiteSpace(email) ||
                !email.Contains('@') ||
                email.Contains(' '))
            {
                throw new ArgumentException($"Invalid email: <{email}>");
            }
            Name = name;
            Email = email;
        }
    }
}
