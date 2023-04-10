using PlanificationEntretien.common.cqrs.middleware.evt;

namespace PlanificationEntretien.common.cqrs.middleware.command;

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