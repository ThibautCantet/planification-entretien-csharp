namespace Cqrs;

public interface EventHandlerReturnVoid : EventHandler
{
    void handle(Event e);
}