namespace Cqrs;

public interface EventHandlerReturnEvent : EventHandler
{
    Event handle(Event e);
}