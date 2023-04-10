using System;

namespace PlanificationEntretien.Common.Cqrs.Event;

public interface IEventHandler
{
    Type ListenTo();

    EventHandlerType GetHandlerType();
}
