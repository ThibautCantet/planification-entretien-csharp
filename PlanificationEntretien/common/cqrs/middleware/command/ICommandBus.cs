using PlanificationEntretien.Common.Cqrs.Command;

namespace PlanificationEntretien.Common.Cqrs.Middleware.Command;

public interface ICommandBus
{
    CommandResponse Dispatch(ICommand command);
}