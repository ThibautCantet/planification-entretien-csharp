namespace Cqrs;

public abstract class EventHandlerCommand<E> : EventHandlerReturnCommand<E> 
    where E : Event 
{
    
    public EventHandlerType getType() {
        return EventHandlerType.COMMAND;
    }

    public abstract C handle<C>(E e) where C : Command;
}
