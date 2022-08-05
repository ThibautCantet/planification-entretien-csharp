using Cqrs;

namespace infrastructure.middleware.command;

public class UnmatchedCommandHandlerException : Exception
{
    public UnmatchedCommandHandlerException(Command command) : base(command.GetType().ToString())
    {
    }
}