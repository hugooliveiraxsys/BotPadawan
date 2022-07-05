using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTerminal.Handlers.Base.Interfaces
{
    public interface IHandler
    {
        public void SetHandler(BaseHandler handler);

        public void HandleRequest(List<string> commands);
    }
}
