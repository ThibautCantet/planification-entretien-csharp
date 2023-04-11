using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class CommandBusDispatcher : ICommandBus
{
    private readonly Dictionary<Type, ICommandHandler<ICommand>> _commandHandlers;

    public CommandBusDispatcher(List<ICommandHandler<ICommand>> commandHandlers)
    {
        this._commandHandlers = commandHandlers.ToDictionary(handler => handler.ListenTo());
    }

    public IEnumerable<Event> Dispatch(ICommand command)
    {
        Type commandType = command.GetType();
        if (_commandHandlers.ContainsKey(commandType))
        {
            var commandHandler = _commandHandlers[commandType];
            return commandHandler.Handle(command);
        }
        else
        {
            throw new UnmatchedCommandHandlerException(command);
        }
    }
}
