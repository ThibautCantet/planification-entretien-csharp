using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.Common.Cqrs.Middleware.Command;

namespace PlanificationEntretien.Common.Cqrs.Application
{
    public abstract class CommandController : ControllerBase
    {
        private ICommandBus _commandBus;
        protected readonly CommandBusFactory _commandBusFactory;

        public CommandController(CommandBusFactory commandBusFactory)
        {
            this._commandBusFactory = commandBusFactory;
        }

        protected ICommandBus GetCommandBus()
        {
            if (_commandBus == null)
            {
                this._commandBus = _commandBusFactory.Build();
            }
            return _commandBus;
        }
    }
}