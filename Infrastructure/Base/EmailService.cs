using Infrastructure.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
