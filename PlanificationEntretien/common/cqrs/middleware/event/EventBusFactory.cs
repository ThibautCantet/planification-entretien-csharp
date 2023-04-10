using System.Collections.Generic;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public class EventBusFactory
{
    private readonly List<IEventHandler> _eventHandlers;

    public EventBusFactory(List<IEventHandler> eventHandlers)
    {
        this._eventHandlers = eventHandlers;
    }

    public IEventBus Build()
    {
        EventBusDispatcher eventBusDispatcher = new EventBusDispatcher(_eventHandlers);

        return new EventBusLogger(eventBusDispatcher);
    }
}
