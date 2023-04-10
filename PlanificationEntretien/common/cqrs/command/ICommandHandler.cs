using System;

namespace PlanificationEntretien.Common.Cqrs.Command;


public interface ICommandHandler<in C, out R> where C : ICommand where R : CommandResponse
{
    R Handle(C command);

    Type ListenTo();
}