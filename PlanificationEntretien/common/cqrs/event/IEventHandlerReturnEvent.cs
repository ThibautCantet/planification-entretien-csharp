namespace PlanificationEntretien.Common.Cqrs.Event;

public interface IEventHandlerReturnEvent<E, NE> : IEventHandler where E : Event where NE : Event
{
    NE Handle(E evt);
}
