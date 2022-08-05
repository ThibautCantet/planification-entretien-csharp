namespace Cqrs;

public abstract class EventHandlerCommand : EventHandlerReturnCommand
{
    public abstract Type listenTo();

    public EventHandlerType getType() {
        return EventHandlerType.COMMAND;
    }

    public abstract Command handle(Event e);
}
