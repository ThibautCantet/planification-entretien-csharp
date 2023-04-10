using System;
using System.Collections.Generic;
using System.Linq;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public class EventBusDispatcher : IEventBus
{
    private readonly HashSet<Event> _publishedEvents;

    public EventBusDispatcher()
    {
        this._publishedEvents = new();
    }

    public ICommand Publish(Event @event)
    {
        IEnumerable<IEventHandler> eventHandlers = GetListeners(@event);

        var commands = new List<ICommand>();
        foreach (var handler in eventHandlers)
        {
            switch (handler.GetHandlerType())
            {
                case EventHandlerType.COMMAND:
                    //TODO handle selon le type
                    //var command = ((IEventHandlerReturnCommand<>)handler).Handle(@event);
                    //if (command != null)
                    //{
                    //    commands.Add(command);
                    //}
                    break;
                case EventHandlerType.EVENT:
                    //TODO handle selon le type
                    //var newEvent = ((IEventHandlerReturnEvent)handler).Handle(@event);
                    //if (newEvent != null)
                    //{
                    //    Publish(newEvent);
                    //    _publishedEvents.Add(newEvent);
                    //}
                    break;
                case EventHandlerType.VOID:
                    //TODO handle selon le type
                    break;
            }
        }

        return commands.FirstOrDefault();
    }

    public void ResetPublishedEvents()
    {
        _publishedEvents.Clear();
    }

    public HashSet<Event> GetPublishedEvents()
    {
        return _publishedEvents;
    }

    private IEnumerable<IEventHandler> GetListeners(Event @event)
    {
        //TODO retourner tous les listener écoutant l'event
        yield break;
    }
}
