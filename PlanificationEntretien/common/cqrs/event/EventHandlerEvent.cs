using System;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public abstract class EventHandlerEvent<E, NE> : IEventHandlerReturnEvent<E, NE> where E : Event where NE : Event
{
    public abstract Type ListenTo();

    public EventHandlerType GetHandlerType()
    {
        return EventHandlerType.EVENT;
    }
    public abstract NE Handle(E evt);
}