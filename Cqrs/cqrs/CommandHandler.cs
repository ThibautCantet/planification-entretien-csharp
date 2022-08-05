namespace Cqrs;

public interface CommandHandler<Event>
{
    CommandResponse<Event> handle(Command command);

    Type listenTo();
}