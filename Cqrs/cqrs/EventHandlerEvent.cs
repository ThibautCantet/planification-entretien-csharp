namespace Cqrs;

public abstract class EventHandlerEvent<E> : EventHandlerReturnEvent<E> 
    where E : Event {

    public EventHandlerType getType() {
        return EventHandlerType.EVENT;
    }
    
    public abstract Type listenTo();

    public abstract Ev handle<Ev>(E e) where Ev : Event;
}
