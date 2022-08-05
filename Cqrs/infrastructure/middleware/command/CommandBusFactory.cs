using System.Data;
using Cqrs;
using EventHandler = Cqrs.EventHandler;

namespace infrastructure.middleware.command;


public class CommandBusFactory {
    
    public CommandBusFactory() 
    {
    }

    protected List<CommandHandler<Event>> getCommandHandlers()
    {
        return new List<CommandHandler<Event>>();
    }

    protected List<EventHandler> getEventHandlers() {
        return new List<EventHandler>();
    }

    public CommandBus build() {
        var commandBusDispatcher = buildCommandBusDispatcher();

        EventBus eventBus = buildEventBus();

        CommandBusLogger commandBusLogger = new CommandBusLogger(commandBusDispatcher);

        return new EventBusDispatcherCommandBus(commandBusDispatcher, eventBus);
    }

    private EventBus buildEventBus() {
        var eventBusFactory = new EventBusFactory(getEventHandlers());
        return eventBusFactory.build();
    }

    private CommandBusDispatcher buildCommandBusDispatcher() {
        List<CommandHandler<Event>> commandHandlers = getCommandHandlers();
        return new CommandBusDispatcher(commandHandlers);
    }
}
