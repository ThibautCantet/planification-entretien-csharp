using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.domain;

namespace com.soat.planification_entretien.common.cqrs.command;

public class CommandResponse
{
    private readonly List<Event> _events;

    public CommandResponse(Event @event)
    {
        this._events = new List<Event> { @event };
    }

    public CommandResponse(List<Event> events)
    {
        this._events = new List<Event>(events);
    }

    public T FindFirst<T>() where T : Event
    {
        return (T)_events.FirstOrDefault(e => e is T);
    }
    
    public bool FindAny<T>() where T : Event
    {
        return _events.Any(e => e is T);
    }

    public List<Event> Events()
    {
        return _events;
    }
}