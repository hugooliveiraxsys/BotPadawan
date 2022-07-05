using ApplicationTerminal.Handlers.Base;
using Infrastructure.Services.ExtractServices;
using Infrastructure.Services.ExtractServices.Base.Interfaces;
using Infrastructure.Services.ExtractServices.Interfaces;
using Models.Entities;
using Models.Extentions;
using Models.Requests;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationTerminal.Handlers.ActionHandlers
{
    public class ExtractHandler : BaseHandler
    {
        private IExtractService _extractService;

        private IPersonExtractService _personExtractService;

        public ExtractHandler()
        {
            _startup = new Startup();

            _personExtractService = _startup.Provider.GetRequiredService<IPersonExtractService>();
        }

        public async override void HandleRequest(List<string> commands)
        {
            string extractCommand = commands.GetNextCommand();
            _handler = null;

            _extractService = null;
            int totalLimit = 0;
            int stepLimit = 0;

            if ("PERSON".Equals(extractCommand))
            {
                totalLimit = int.Parse(commands.GetNextCommand());
                stepLimit = int.Parse(commands.GetNextCommand());
                _extractService = _personExtractService;
            }
            else if ("ID".Equals(extractCommand))
            {
                User user = new User();
                Console.WriteLine($"Extraindo o id: {commands[0]}");
                PersonApiRepository getApiRepository = new PersonApiRepository();
                user = await getApiRepository.GetContentAsync(commands[0]);
                Console.WriteLine("-------------");
                Console.WriteLine(user.Nome);
                Console.WriteLine("-------------");
            }
            else
            {
                _extractService = null;
            }
            if (_extractService == null)
            {
                Console.WriteLine("Parametro invalido");
                return;
            }
            await _extractService.ProcessAsync(totalLimit, stepLimit);
            base.HandleRequest(commands);
        }
    }
}
