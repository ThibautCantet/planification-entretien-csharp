using EventHandler = Cqrs.EventHandler;

public class EventBusFactory
{
    private List<EventHandler> _eventHandlers;

    public EventBusFactory(List<EventHandler> eventHandlers) {
        _eventHandlers = eventHandlers;
    }

    public EventBus build() {
        EventBus eventBusDispatcher = new EventBusDispatcher(_eventHandlers);

        return new EventBusLogger(eventBusDispatcher);
    }

}
