namespace Cqrs;

public abstract class EventHandlerEvent : EventHandlerReturnEvent
{
    public EventHandlerType getType() {
        return EventHandlerType.EVENT;
    }

    public abstract Type listenTo();
    public abstract Event handle(Event e);

}
