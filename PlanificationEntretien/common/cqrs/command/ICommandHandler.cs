using System;
using System.Collections.Generic;
using PlanificationEntretien.domain;

namespace com.soat.planification_entretien.common.cqrs.command;


public interface ICommandHandler<C>
{
    IEnumerable<Event> Handle(C command);

    Type ListenTo();
}