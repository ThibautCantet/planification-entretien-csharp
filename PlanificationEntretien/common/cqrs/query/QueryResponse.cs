using System;
using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.domain;

namespace com.soat.planification_entretien.common.cqrs.query;

public interface IQueryResponse<out T>
{
    T Value { get; }
    List<Event> Events { get; }
}

public class QueryResponse<T> : IQueryResponse<T>
{
    private readonly T _value;

    public T Value => _value;

    public List<Event> Events { get; }

    public QueryResponse(T value, Event @event)
    {
        _value = value;
        Events = new List<Event> { @event };
    }

    public QueryResponse(T value, List<Event> events)
    {
        _value = value;
        Events = new List<Event>(events);
    }

    public Event FindFirst(Type clazz)
    {
        return Events.FirstOrDefault(e => e.GetType() == clazz);
    }
}