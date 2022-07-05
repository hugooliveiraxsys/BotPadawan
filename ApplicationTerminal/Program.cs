using ApplicationTerminal.Handlers;
using Models.Extentions;
using System;
using System.Collections.Generic;

namespace ApplicationTerminal
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------Bot Padawan-------");
            while (true) {
                string commandText = Console.ReadLine();
                if (commandText == String.Empty) break;
                List<string> commands = commandText.ToCommands();

                ActionHandler actionHandler = new ActionHandler();

                actionHandler.HandleRequest(commands);

            }
            Console.WriteLine("Finalizado");
        }
    }
}
