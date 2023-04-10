using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public interface IEventHandlerReturnEvent : IEventHandler
{
    Event Handle(Event evt);
}
