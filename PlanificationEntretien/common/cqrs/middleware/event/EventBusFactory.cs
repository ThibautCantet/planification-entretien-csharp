using PlanificationEntretien.common.cqrs.middleware.evt;

namespace PlanificationEntretien.Common.Cqrs.Middleware.Event;

public class EventBusFactory
{

    public IEventBus Build()
    {
        EventBusDispatcher eventBusDispatcher = new EventBusDispatcher();

        return new EventBusLogger(eventBusDispatcher);
    }
}
