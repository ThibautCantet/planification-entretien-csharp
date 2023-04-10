using System;
using PlanificationEntretien.Common.Cqrs.Command;

namespace PlanificationEntretien.Common.Cqrs.Middleware.Command;

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
