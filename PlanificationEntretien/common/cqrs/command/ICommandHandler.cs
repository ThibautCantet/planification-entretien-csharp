using System;

namespace com.soat.planification_entretien.common.cqrs.command;


public interface ICommandHandler
{
    CommandResponse Handle(ICommand command);

    Type ListenTo();
}