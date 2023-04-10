using System.Collections.Generic;
using System.Linq;

namespace PlanificationEntretien.Common.Cqrs.Command;

public class CommandResponse
{
    private readonly List<Event.Event> _events;

    public CommandResponse(Event.Event @event)
    {
        this._events = new List<Event.Event> { @event };
    }

    public CommandResponse(List<Event.Event> events)
    {
        this._events = new List<Event.Event>(events);
    }

    public T FindFirst<T>() where T : Event.Event
    {
        return (T)_events.FirstOrDefault(e => e is T);
    }
    
    public bool FindAny<T>() where T : Event.Event
    {
        return _events.Any(e => e is T);
    }

    public List<Event.Event> Events()
    {
        return _events;
    }
}