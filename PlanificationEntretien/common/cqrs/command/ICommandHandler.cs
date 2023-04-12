using System;

namespace com.soat.planification_entretien.common.cqrs.command;


public interface ICommandHandler<in C, out R> where C : ICommand where R : CommandResponse
{
    R Handle(C command);

    Type ListenTo();
}