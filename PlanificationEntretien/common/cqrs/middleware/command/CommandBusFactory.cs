using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.candidat.domain_service;
using PlanificationEntretien.common.cqrs.middleware.evt;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.entretien.query.application;
using PlanificationEntretien.recruteur.application_service.application;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class CommandBusFactory
{
    private readonly ICandidatRepository _candidatRepository;
    private readonly CandidatFactory _candidatFactory;
    private readonly IEntretienRepository _entretienRepository;
    private readonly IEmailService _emailService;
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly IRecruteurDao _recruteurDao;
    private readonly IEntretienProjectionDao _entretienProjectionDao;

    public CommandBusFactory(ICandidatRepository candidatRepository, CandidatFactory candidatFactory, IEntretienRepository entretienRepository, IEmailService emailService, IRecruteurRepository recruteurRepository, IRecruteurDao recruteurDao, IEntretienProjectionDao entretienProjectionDao)
    {
        _candidatRepository = candidatRepository;
        _candidatFactory = candidatFactory;
        _entretienRepository = entretienRepository;
        _emailService = emailService;
        _recruteurRepository = recruteurRepository;
        _recruteurDao = recruteurDao;
        _entretienProjectionDao = entretienProjectionDao;
    }

    public ICommandBus Build()
    {
        CommandBusDispatcher commandBusDispatcher = new CommandBusDispatcher(
            _candidatRepository,
            _candidatFactory,
            _entretienRepository,
            _emailService,
            _recruteurRepository);

        IEventBus eventBus = BuildEventBus();

        CommandBusLogger commandBusLogger = new CommandBusLogger(commandBusDispatcher);

        return new EventBusDispatcherCommandBus(commandBusLogger, eventBus);
    }

    private IEventBus BuildEventBus()
    {
        EventBusFactory eventBusFactory = new EventBusFactory(_recruteurDao, _recruteurRepository, _entretienProjectionDao);
        return eventBusFactory.Build();
    }
}