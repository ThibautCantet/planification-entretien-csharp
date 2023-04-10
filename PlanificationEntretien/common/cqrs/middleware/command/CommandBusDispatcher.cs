using PlanificationEntretien.Common.Cqrs.Command;

namespace PlanificationEntretien.Common.Cqrs.Middleware.Command;

public class CommandBusDispatcher : ICommandBus
{
    public CommandResponse Dispatch(ICommand command)
    {
        throw new UnmatchedCommandHandlerException(command);
    }
}
