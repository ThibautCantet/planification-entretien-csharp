using System.Collections.Generic;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public interface ICommandBus
{
    CommandResponse Dispatch(ICommand command);
}