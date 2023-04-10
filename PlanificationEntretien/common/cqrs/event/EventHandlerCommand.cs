using System;
using PlanificationEntretien.Common.Cqrs.Command;
using PlanificationEntretien.common.cqrs.middleware.evt;

namespace PlanificationEntretien.Common.Cqrs.Event;

public abstract class EventHandlerCommand<E, C> : IEventHandlerReturnCommand<E, C> where E : Event where C : ICommand
{
    public abstract Type ListenTo();

    public EventHandlerType GetHandlerType()
    {
        return EventHandlerType.COMMAND;
    }
    public abstract C Handle(E @event);
}