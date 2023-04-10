using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.Common.Cqrs.Command;
using PlanificationEntretien.Common.Cqrs.Event;
using PlanificationEntretien.common.cqrs.middleware.evt;

namespace PlanificationEntretien.Common.Cqrs.Middleware.Event;

public class EventBusDispatcher : IEventBus
{
    private readonly HashSet<Cqrs.Event.Event> _publishedEvents;

    public EventBusDispatcher()
    {
        this._publishedEvents = new();
    }

    public ICommand Publish(Cqrs.Event.Event @event)
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

    public HashSet<Cqrs.Event.Event> GetPublishedEvents()
    {
        return _publishedEvents;
    }

    private IEnumerable<IEventHandler> GetListeners(Cqrs.Event.Event @event)
    {
        //TODO retourner tous les listener écoutant l'event
        yield break;
    }
}
