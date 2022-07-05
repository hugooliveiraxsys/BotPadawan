using ApplicationTerminal.Handlers.Base.Interfaces;
using System;
using System.Collections.Generic;

namespace ApplicationTerminal.Handlers.Base
{
    public abstract class BaseHandler : IHandler
    {
        protected BaseHandler _handler;

        public void SetHandler(BaseHandler handler)
        {
            _handler = handler;
        }

        public virtual void HandleRequest(List<string> commands)
        {
            if (_handler != null)
            {
                _handler.HandleRequest(commands);
            }
        }
    }
}
