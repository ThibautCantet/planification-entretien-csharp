namespace PlanificationEntretien.Common.Cqrs.Event;

public interface IEventHandlerReturnVoid<E> : IEventHandler where E : Event
{
    void Handle(E evt);
}
