namespace Cqrs;

public interface CommandHandler<C, R, E> 
    where C : Command
    where R : CommandResponse<E>
    where E : Event
{
    R handle(C command);

    Type listenTo();
}