
using ApplicationTerminal.Handlers.ActionHandlers;
using ApplicationTerminal.Handlers.Base;
using Models.Extentions;
using System;
using System.Collections.Generic;

namespace ApplicationTerminal.Handlers
{
    public class ActionHandler : BaseHandler
    {
        public override void HandleRequest(List<string> commands)
        {
            string actionCommand = commands.GetNextCommand();
            _handler = null;

            if ("HELP".Equals(actionCommand))
            {
                Console.WriteLine("-------------");
                Console.WriteLine("HELP");
                Console.WriteLine("EXTRACT");
                Console.WriteLine("SEND SMS or EMAIL");
                Console.WriteLine("CLEAR");
                Console.WriteLine("Blank Space to EXIT");
                Console.WriteLine("-------------");

            }
            else if ("EXTRACT".Equals(actionCommand))
            {
                ExtractHandler extractHandler = new ExtractHandler();
                extractHandler.HandleRequest(commands);
            }
            else if ("SEND".Equals(actionCommand))
            {
                SendHandler sendHandler = new SendHandler();
                sendHandler.HandleRequest(commands);
                Console.WriteLine("-------------");

            }
            else if ("CLEAR".Equals(actionCommand))
            {
                Console.Clear();
                Console.WriteLine("-------------");
            }
            else
            {
                Console.WriteLine("Como vc é burro");
                _handler = null;
                Console.WriteLine("-------------");
            }
            base.HandleRequest(commands);
        }
    }

}