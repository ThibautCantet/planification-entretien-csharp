using PlanificationEntretien.Common.Cqrs.Command;

namespace PlanificationEntretien.Common.Cqrs.Event;

public interface IEventHandlerReturnCommand<E, C> : IEventHandler where E : Event where C : ICommand
{
    C Handle(E @event);
}
