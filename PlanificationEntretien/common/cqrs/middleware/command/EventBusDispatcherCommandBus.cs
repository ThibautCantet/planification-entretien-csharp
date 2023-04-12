using System.Collections;
using System.Collections.Generic;
using System.Linq;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.common.cqrs.middleware.evt;
using PlanificationEntretien.domain;

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

        ICommand eventCommand = PublishEvent(commandResponse.Events()); // Publish commandResponse from the command response

        if (eventCommand != null)
        {
            var eventCommandResponse = this.Dispatch(eventCommand); // Dispatch the event command recursively
            commandResponse.Events().AddRange(eventCommandResponse.Events()); // Add commandResponse from the event command to the command response
            return commandResponse;
        }

        return BuildCommandResponseWithGeneratedEvents(commandResponse); // Add published commandResponse from the EventBus to the command response
    }

    private ICommand PublishEvent(List<Event> events)
    {
        ICommand command = null;
        events.ForEach(e => command = _eventBus.Publish(e)); // Publish commandResponse to the EventBus and store the event command
        return command;
    }

    private CommandResponse BuildCommandResponseWithGeneratedEvents(CommandResponse commandResponse)
    {
        commandResponse.Events().AddRange(_eventBus.GetPublishedEvents()); // Add published commandResponse from the EventBus to the command response
        return commandResponse;
    }
}
