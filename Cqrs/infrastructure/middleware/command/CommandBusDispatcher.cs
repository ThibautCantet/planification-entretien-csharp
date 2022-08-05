using Cqrs;

namespace infrastructure.middleware.command;

public class CommandBusDispatcher: CommandBus
{
    private Dictionary<Type, CommandHandler<Event>> _commandHandlers;

    public CommandBusDispatcher(List<CommandHandler<Event>> commandHandlers) {
        //this._commandHandlers = commandHandlers.Select(p => new { id = p.listenTo(), person = p })
        //    .ToDictionary(x => x.id, x => x.person);
        //
        //().collect(Collectors
        //    .toMap(CommandHandler::listenTo, commandHandler->commandHandler));
    }

    public CommandResponse<Event> dispatch(Command command)
    {
        _commandHandlers.TryGetValue(command.GetType(), out var commandHandler);
        if (commandHandler != null)
        {
            return commandHandler.handle(command);
        }
        throw new UnmatchedCommandHandlerException(command);
    }
}