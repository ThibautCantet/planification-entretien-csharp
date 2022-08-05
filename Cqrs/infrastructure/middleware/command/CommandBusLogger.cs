using Cqrs;

namespace infrastructure.middleware.command;

public class CommandBusLogger : CommandBus {

    private readonly CommandBus _commandBus;

    public CommandBusLogger(CommandBus commandBus) {
        this._commandBus = commandBus;
    }

    public CommandResponse<Event> dispatch(Command command)
    {
        Console.Out.WriteLine(command);
        var commandResponse = _commandBus.dispatch(command);
        return commandResponse;
    }

}
