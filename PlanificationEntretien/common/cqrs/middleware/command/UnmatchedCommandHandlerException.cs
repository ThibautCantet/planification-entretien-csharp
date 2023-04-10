using System;
using PlanificationEntretien.Common.Cqrs.Command;

namespace PlanificationEntretien.Common.Cqrs.Middleware.Command;
public class UnmatchedCommandHandlerException : Exception
{
    public UnmatchedCommandHandlerException(ICommand command)
        : base($"No matching command handler found for command of type {command.GetType().FullName}")
    {
    }
}