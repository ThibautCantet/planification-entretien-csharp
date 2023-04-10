using System.Collections.Generic;
using PlanificationEntretien.Common.Cqrs.Command;

namespace PlanificationEntretien.Common.Cqrs.Middleware.Event;

public interface IEventBus
{
    ICommand Publish(Cqrs.Event.Event @event);

    void ResetPublishedEvents();

    HashSet<Cqrs.Event.Event> GetPublishedEvents();
}
