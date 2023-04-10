using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public interface IEventHandlerReturnVoid : IEventHandler
{
    void Handle(Event evt);
}
