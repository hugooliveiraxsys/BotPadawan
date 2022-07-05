using Infrastructure.Services.Base.Interfaces;
using System.Threading.Tasks;

namespace Infrastructure.Services.ExtractServices.Base.Interfaces
{
    public interface IExtractService : IService
    {
        public Task ProcessAsync(int totalLimit, int stepLimit);
    }
}