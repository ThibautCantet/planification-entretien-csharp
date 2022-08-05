using Cqrs;

namespace infrastructure.middleware.command;


public interface CommandBus {
    CommandResponse<Event> dispatch(Command command);
}
