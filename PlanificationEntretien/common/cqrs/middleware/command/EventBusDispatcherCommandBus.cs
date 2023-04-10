using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.common.cqrs.middleware.evt;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class EventBusDispatcherCommandBus : ICommandBus
{
    private readonly ICommandBus _commandBus;
    private readonly IEventBus _eventBus;

    public EventBusDispatcherCommandBus(ICommandBus commandBus, IEventBus eventBus)
    {
        this._commandBus = commandBus;
        this._eventBus = eventBus;
    }

    public CommandResponse Dispatch(ICommand command)
    {
        var commandResponse = _commandBus.Dispatch(command); // Dispatch the command using the wrapped CommandBus

        ICommand eventCommand = PublishEvent(commandResponse); // Publish events from the command response

        if (eventCommand != null)
        {
            var dispatch = this.Dispatch(eventCommand); // Dispatch the event command recursively
            commandResponse.Events().AddRange(dispatch.Events()); // Add events from the event command to the command response
            return commandResponse;
        }

        return BuildCommandResponseWithGeneratedEvents(commandResponse); // Add published events from the EventBus to the command response
    }

    private ICommand PublishEvent<R>(R commandResponse) where R : CommandResponse
    {
        ICommand command = null;
        commandResponse.Events().ForEach(e => command = _eventBus.Publish(e)); // Publish events to the EventBus and store the event command
        return command;
    }

    private R BuildCommandResponseWithGeneratedEvents<R>(R dispatchedCommandResponse) where R : CommandResponse
    {
        dispatchedCommandResponse.Events().AddRange(_eventBus.GetPublishedEvents()); // Add published events from the EventBus to the command response
        return dispatchedCommandResponse;
    }
}
