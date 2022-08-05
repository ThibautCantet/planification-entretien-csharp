namespace Cqrs;

public abstract class EventHandlerVoid : EventHandlerReturnVoid
{
    public abstract Type listenTo();

    public EventHandlerType getType()
    {
        return EventHandlerType.VOID;
    }

    public abstract void handle(Event e);
}