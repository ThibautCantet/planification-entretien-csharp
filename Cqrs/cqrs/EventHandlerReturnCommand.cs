namespace Cqrs;


public interface EventHandlerReturnCommand<E> where E : Event {

     C handle<C>(E e) where C : Command;

}
