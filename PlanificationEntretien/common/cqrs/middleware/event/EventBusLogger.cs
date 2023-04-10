using System;
using System.Collections.Generic;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public class EventBusLogger : IEventBus
{
    private readonly IEventBus _eventBus;

    public EventBusLogger(IEventBus eventBus)
    {
        this._eventBus = eventBus;
    }

    public ICommand Publish(Event @event)
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

    public HashSet<Event> GetPublishedEvents()
    {
        return _eventBus.GetPublishedEvents();
    }
}
