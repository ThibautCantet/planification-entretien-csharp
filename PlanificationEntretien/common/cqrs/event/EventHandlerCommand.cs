using System;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public abstract class EventHandlerCommand<E, C> : IEventHandlerReturnCommand<E, C> where E : Event where C : ICommand
{
    public abstract Type ListenTo();

    public EventHandlerType GetHandlerType()
    {
        return EventHandlerType.COMMAND;
    }
    public abstract C Handle(E @event);
}