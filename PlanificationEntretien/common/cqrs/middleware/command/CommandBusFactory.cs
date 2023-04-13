using PlanificationEntretien.application_service;
using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.candidat.domain_service;
using PlanificationEntretien.common.cqrs.middleware.evt;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.recruteur.application_service.application;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class CommandBusFactory
{
    private readonly ICandidatRepository _candidatRepository;
    private readonly CandidatFactory _candidatFactory;
    private readonly IEntretienRepository _entretienRepository;
    private readonly IEmailService _emailService;
    private readonly MessageBus _messsageBus;
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly IRecruteurDao _recruteurDao;

    public CommandBusFactory(ICandidatRepository candidatRepository, CandidatFactory candidatFactory, IEntretienRepository entretienRepository, IEmailService emailService, MessageBus messsageBus, IRecruteurRepository recruteurRepository, IRecruteurDao recruteurDao)
    {
        _candidatRepository = candidatRepository;
        _candidatFactory = candidatFactory;
        _entretienRepository = entretienRepository;
        _emailService = emailService;
        _messsageBus = messsageBus;
        _recruteurRepository = recruteurRepository;
        _recruteurDao = recruteurDao;
    }

    public ICommandBus Build()
    {
        CommandBusDispatcher commandBusDispatcher = new CommandBusDispatcher(
            _candidatRepository,
            _candidatFactory,
            _entretienRepository,
            _emailService,
            _messsageBus,
            _recruteurRepository);

        IEventBus eventBus = BuildEventBus();

        CommandBusLogger commandBusLogger = new CommandBusLogger(commandBusDispatcher);

        return new EventBusDispatcherCommandBus(commandBusLogger, eventBus);
    }

    private IEventBus BuildEventBus()
    {
        EventBusFactory eventBusFactory = new EventBusFactory(_recruteurDao);
        return eventBusFactory.Build();
    }
}