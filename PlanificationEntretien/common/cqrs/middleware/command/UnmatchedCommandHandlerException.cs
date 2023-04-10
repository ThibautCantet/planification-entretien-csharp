using System;
using com.soat.planification_entretien.common.cqrs.command;

namespace PlanificationEntretien.common.cqrs.middleware.command;
public class UnmatchedCommandHandlerException : Exception
{
    public UnmatchedCommandHandlerException(ICommand command)
        : base($"No matching command handler found for command of type {command.GetType().FullName}")
    {
    }
}