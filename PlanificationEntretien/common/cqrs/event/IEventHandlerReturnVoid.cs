using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public interface IEventHandlerReturnVoid<E> : IEventHandler where E : Event
{
    void Handle(E evt);
}
