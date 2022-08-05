using Cqrs;

namespace infrastructure.middleware.command;

public class EventBusDispatcherCommandBus : CommandBus {
    private CommandBus commandBus;
    private EventBus eventBus;

    public EventBusDispatcherCommandBus(CommandBus commandBus, EventBus eventBus) {
        this.commandBus = commandBus;
        this.eventBus = eventBus;
    }

    public  CommandResponse<Event> dispatch(Command command)
    {
        var commandResponse = commandBus.dispatch(command);

        Command eventCommand = publishEvent(commandResponse);

        if (eventCommand != null) {
            var dispatch = this.dispatch(eventCommand);
            commandResponse.events().AddRange(dispatch.events());
            return commandResponse;
        }

        return buildCommandResponseWishGeneratedEvents(commandResponse);
    }

    private Command publishEvent(CommandResponse<Event> commandResponse)
    {
        List<Command> command = new List<Command>();
        commandResponse.events().ForEach(e =>
        {
            command.Add(eventBus.publish(e));
        });
        
        return command.First();
    }


    private CommandResponse<Event> buildCommandResponseWishGeneratedEvents(CommandResponse<Event> dispatchedCommandResponse) {
        dispatchedCommandResponse.events().AddRange(eventBus.getPublishedEvents());
        return dispatchedCommandResponse;
    }
}
