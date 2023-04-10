using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

using System;
using System.Collections.Generic;
using System.Linq;

public class EventBusDispatcher : IEventBus
{
    private readonly Dictionary<Type, List<IEventHandler>> eventHandlers;
    private readonly HashSet<Event> publishedEvents;

    public EventBusDispatcher(List<IEventHandler> eventHandlers)
    {
        this.eventHandlers = eventHandlers.GroupBy(x => x.ListenTo())
            .ToDictionary(grouping => grouping.Key, grouping => grouping.ToList());
        this.publishedEvents = new();
    }

    public ICommand Publish(Event @event)
    {
        List<IEventHandler> eventHandlers = GetListeners(@event);

        var commands = new List<ICommand>();
        foreach (var handler in eventHandlers)
        {
            switch (handler.GetType())
            {
                case EventHandlerType.COMMAND:
                    var command = ((IEventHandlerReturnCommand)handler).Handle(@event);
                    if (command != null)
                    {
                        commands.Add(command);
                    }
                    break;
                case EventHandlerType.EVENT:
                    var newEvent = ((IEventHandlerReturnEvent)handler).Handle(@event);
                    if (newEvent != null)
                    {
                        Publish(newEvent);
                        publishedEvents.Add(newEvent);
                    }
                    break;
                case EventHandlerType.VOID:
                    ((IEventHandlerReturnVoid)handler).Handle(@event);
                    break;
            }
        }

        return commands.FirstOrDefault();
    }

    public void ResetPublishedEvents()
    {
        publishedEvents.Clear();
    }

    public HashSet<Event> GetPublishedEvents()
    {
        return publishedEvents;
    }

    private List<IEventHandler> GetListeners(Event @event)
    {
        return eventHandlers.Where(entry => entry.Key.IsInstanceOfType(@event))
            .SelectMany(entry => entry.Value)
            .ToList();
    }
}
