using System.Collections.Generic;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.common.cqrs.middleware.evt;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class CommandBusFactory
{

    protected List<ICommandHandler> GetCommandHandlers()
    {
        return new List<ICommandHandler>
        {
        };
    }

    protected List<IEventHandler> GetEventHandlers()
    {
        return new List<IEventHandler>();
    }

    public ICommandBus Build()
    {
        CommandBusDispatcher commandBusDispatcher = BuildCommandBusDispatcher();

        IEventBus eventBus = BuildEventBus();

        CommandBusLogger commandBusLogger = new CommandBusLogger(commandBusDispatcher);

        return new EventBusDispatcherCommandBus(commandBusLogger, eventBus);
    }

    private IEventBus BuildEventBus()
    {
        EventBusFactory eventBusFactory = new EventBusFactory(GetEventHandlers());
        return eventBusFactory.Build();
    }

    private CommandBusDispatcher BuildCommandBusDispatcher()
    {
        var commandHandlers = GetCommandHandlers();
        return new CommandBusDispatcher(commandHandlers);
    }
}
