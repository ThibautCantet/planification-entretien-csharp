using com.soat.planification_entretien.common.cqrs.command;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public interface ICommandBus
{
    CommandResponse Dispatch(ICommand command);
}