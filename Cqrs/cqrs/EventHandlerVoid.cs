namespace Cqrs;

public abstract class EventHandlerVoid<E> : EventHandlerReturnVoid<E> where E : Event
{
    public EventHandlerType getType()
    {
        return EventHandlerType.VOID;
    }

    public abstract void handle(E e);
}