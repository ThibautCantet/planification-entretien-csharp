using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public interface IEventHandlerReturnCommand<E, C> : IEventHandler where E : Event where C : ICommand
{
    C Handle(E @event);
}
