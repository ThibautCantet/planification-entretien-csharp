using Microsoft.CodeAnalysis;
using PlanificationEntretien.domain;

namespace com.soat.planification_entretien.common.cqrs.command;

using System;
using System.Collections.Generic;
using System.Linq;

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

    public Event FindFirst(Type clazz)
    {
        return _events.FirstOrDefault(e => e.GetType() == clazz);
    }

    public List<Event> Events()
    {
        return _events;
    }
}