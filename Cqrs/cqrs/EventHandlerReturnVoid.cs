namespace Cqrs;

public interface EventHandlerReturnVoid<E> where E: Event
{
    void handle(E e);
}