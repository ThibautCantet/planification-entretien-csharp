using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.candidat.domain_service;
using PlanificationEntretien.common.cqrs.middleware.evt;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class CommandBusFactory
{
    private readonly ICandidatRepository _candidatRepository;
    private readonly CandidatFactory _candidatFactory;

    public CommandBusFactory(ICandidatRepository candidatRepository, CandidatFactory candidatFactory)
    {
        _candidatRepository = candidatRepository;
        _candidatFactory = candidatFactory;
    }

    public ICommandBus Build()
    {
        CommandBusDispatcher commandBusDispatcher = new CommandBusDispatcher(
            _candidatRepository,
            _candidatFactory);

        IEventBus eventBus = BuildEventBus();

        CommandBusLogger commandBusLogger = new CommandBusLogger(commandBusDispatcher);

        return new EventBusDispatcherCommandBus(commandBusLogger, eventBus);
    }

    private IEventBus BuildEventBus()
    {
        EventBusFactory eventBusFactory = new EventBusFactory();
        return eventBusFactory.Build();
    }
}