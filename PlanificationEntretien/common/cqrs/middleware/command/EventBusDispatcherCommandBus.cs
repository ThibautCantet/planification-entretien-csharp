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

    public IEnumerable<Event> Dispatch(ICommand command)
    {
        var commandEvents = _commandBus.Dispatch(command).ToList(); // Dispatch the command using the wrapped CommandBus

        ICommand eventCommand = PublishEvent(commandEvents); // Publish events from the command response

        if (eventCommand != null)
        {
            var events = this.Dispatch(eventCommand); // Dispatch the event command recursively
            commandEvents.AddRange(events); // Add events from the event command to the command response
            return commandEvents;
        }

        return BuildCommandResponseWithGeneratedEvents(commandEvents); // Add published events from the EventBus to the command response
    }

    private ICommand PublishEvent(List<Event> events)
    {
        ICommand command = null;
        events.ForEach(e => command = _eventBus.Publish(e)); // Publish events to the EventBus and store the event command
        return command;
    }

    private List<Event> BuildCommandResponseWithGeneratedEvents(List<Event> events)
    {
        events.AddRange(_eventBus.GetPublishedEvents()); // Add published events from the EventBus to the command response
        return events;
    }
}
