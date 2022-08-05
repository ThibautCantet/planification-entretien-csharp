namespace Cqrs;

public interface EventHandler
{
    Type listenTo();

    EventHandlerType getType();
}