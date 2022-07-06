using Infrastructure.Services.Base.Interfaces;
using System;

namespace Infrastructure.Base
{
    public class EmailService : IService
    {
        public void DoAction(int quantity)
        {
            Console.WriteLine($"Enviando {quantity} emails!");
        }
    }
}
