using PlanificationEntretien.Common.Cqrs.Middleware.Event;
using PlanificationEntretien.common.cqrs.middleware.evt;

namespace PlanificationEntretien.Common.Cqrs.Middleware.Command;

public class CommandBusFactory
{

    public CommandBusFactory()
    {
    }

    public ICommandBus Build()
    {
        CommandBusDispatcher commandBusDispatcher = new CommandBusDispatcher();

        IEventBus eventBus = BuildEventBus();

        CommandBusLogger commandBusLogger = new CommandBusLogger(commandBusDispatcher);

        return new EventBusDispatcherCommandBus(commandBusLogger, eventBus);
    }

    private IEventBus BuildEventBus()
    {
        EventBusFactory eventBusFactory = new EventBusFactory();
        return eventBusFactory.Build();
    }
}