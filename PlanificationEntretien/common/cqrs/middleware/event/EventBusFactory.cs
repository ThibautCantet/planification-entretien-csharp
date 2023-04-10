namespace PlanificationEntretien.common.cqrs.middleware.evt;

public class EventBusFactory
{

    public IEventBus Build()
    {
        EventBusDispatcher eventBusDispatcher = new EventBusDispatcher();

        return new EventBusLogger(eventBusDispatcher);
    }
}
