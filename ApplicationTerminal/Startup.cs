using Infrastructure.Services.ExtractServices;
using Infrastructure.Services.ExtractServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.Interfaces;
using System;

namespace ApplicationTerminal
{
    public class Startup
    {
        private readonly IServiceProvider provider;

        public IServiceProvider Provider => provider;

        public Startup ()
        {
            IServiceCollection services = new ServiceCollection ();
            
            services.AddScoped<IPersonExtractService, PersonExtractService>();

            services.AddScoped<IPersonApiRepository, PersonApiRepository>();

            provider = services.BuildServiceProvider();
        }
    }
}
