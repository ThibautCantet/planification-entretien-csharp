namespace Cqrs;

public interface EventHandlerReturnEvent<E> : EventHandler<E> where E : Event
{
    Ev handle<Ev>(E e) where Ev : Event;
}