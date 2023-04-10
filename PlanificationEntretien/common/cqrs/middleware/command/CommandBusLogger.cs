using System;
using com.soat.planification_entretien.common.cqrs.command;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class CommandBusLogger : ICommandBus
{
    private readonly ICommandBus _commandBus;

    public CommandBusLogger(ICommandBus commandBus)
    {
        this._commandBus = commandBus;
    }
    
    public CommandResponse Dispatch(ICommand command)
    {
        Console.WriteLine(command.ToString()); // Log the command before dispatching
        var commandResponse = this._commandBus.Dispatch(command); // Call dispatch on the wrapped CommandBus
        return commandResponse;
    }
}
