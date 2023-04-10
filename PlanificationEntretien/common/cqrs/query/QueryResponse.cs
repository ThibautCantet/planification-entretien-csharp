using System;
using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.domain;

namespace com.soat.planification_entretien.common.cqrs.query;

public class QueryResponse
{
    private readonly List<Event> _events;

    public QueryResponse(Event @event)
    {
        this._events = new List<Event> { @event };
    }

    public QueryResponse(List<Event> events)
    {
        this._events = new List<Event>(events);
    }

    public Event FindFirst(Type clazz)
    {
        return _events.FirstOrDefault(e => e.GetType() == clazz);
    }

    public List<Event> Events()
    {
        return _events;
    }
}