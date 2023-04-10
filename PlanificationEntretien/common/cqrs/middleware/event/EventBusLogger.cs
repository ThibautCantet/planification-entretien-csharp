using System;
using System.Collections.Generic;
using PlanificationEntretien.Common.Cqrs.Command;
using PlanificationEntretien.common.cqrs.middleware.evt;

namespace PlanificationEntretien.Common.Cqrs.Middleware.Event;

public class EventBusLogger : IEventBus
{
    private readonly IEventBus _eventBus;

    public EventBusLogger(IEventBus eventBus)
    {
        this._eventBus = eventBus;
    }

    public ICommand Publish(Cqrs.Event.Event @event)
    {
        ICommand command = _eventBus.Publish(@event);
        if (@event != null)
        {
            Console.WriteLine(@event.ToString());
        }
        return command;
    }

    public void ResetPublishedEvents()
    {
        _eventBus.ResetPublishedEvents();
    }

    public HashSet<Cqrs.Event.Event> GetPublishedEvents()
    {
        return _eventBus.GetPublishedEvents();
    }
}
