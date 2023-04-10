using com.soat.planification_entretien.common.cqrs.command;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class CommandBusDispatcher : ICommandBus
{
    public CommandResponse Dispatch(ICommand command)
    {
        throw new UnmatchedCommandHandlerException(command);
    }
}
