namespace Cqrs;

public interface EventHandler<E> where E : Event
{
    //Event handle(T event);

    Type listenTo();

    EventHandlerType getType();
}