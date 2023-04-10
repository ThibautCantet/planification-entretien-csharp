using System;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public interface IEventHandler
{
    Type ListenTo();

    EventHandlerType GetType();
}
