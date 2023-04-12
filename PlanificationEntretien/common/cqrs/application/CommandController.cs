
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.common.cqrs.middleware.command;

namespace com.soat.planification_entretien.common.cqrs.application
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