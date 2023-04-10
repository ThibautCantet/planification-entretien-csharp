using System;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public abstract class EventHandlerVoid<E> : IEventHandlerReturnVoid<E> where E : Event
{
    public abstract Type ListenTo();

    public EventHandlerType GetHandlerType()
    {
        return EventHandlerType.VOID;
    }
    public abstract void Handle(E evt);
}