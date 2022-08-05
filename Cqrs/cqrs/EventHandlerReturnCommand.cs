namespace Cqrs;


public interface EventHandlerReturnCommand : EventHandler
{

     Command handle(Event e);

}
