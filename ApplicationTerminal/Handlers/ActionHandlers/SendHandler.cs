using ApplicationTerminal.Handlers.Base;
using Models.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTerminal.Handlers.ActionHandlers
{
    public class SendHandler : BaseHandler
    {
        public override void HandleRequest(List<string> commands)
        {
            string sendCommand = commands.GetNextCommand();

            _handler = null;

            if ("EMAIL".Equals(sendCommand))
            {
                Console.WriteLine("Enviando email");
            }
            else if ("SMS".Equals(sendCommand))
            {
                Console.WriteLine("Enviando email");
            }
            else
            {
                Console.WriteLine("Erro ao encontrar o operador!");
            }
            base.HandleRequest(commands);
        }
    }
}
