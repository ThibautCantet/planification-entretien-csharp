using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public interface IEventHandlerReturnCommand : IEventHandler
{
    ICommand Handle(Event @event);
}
