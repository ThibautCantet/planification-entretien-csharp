using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public interface IEventHandlerReturnEvent<E, NE> : IEventHandler where E : Event where NE : Event
{
    NE Handle(E evt);
}
