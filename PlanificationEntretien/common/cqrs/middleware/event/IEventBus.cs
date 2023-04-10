using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

using System.Collections.Generic;

public interface IEventBus
{
    ICommand Publish(Event @event);

    void ResetPublishedEvents();

    HashSet<Event> GetPublishedEvents();
}
