using System.Collections.Generic;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.application_service;
using PlanificationEntretien.candidat.application_service;
using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.candidat.domain_service;
using PlanificationEntretien.common.cqrs.middleware.evt;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.recruteur.application_service;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class CommandBusFactory
{
    private readonly IEntretienRepository _entretienRepository;
    private readonly IEmailService _emailService;
    private readonly MessageBus _messsageBus;
    private readonly ICandidatRepository _candidatRepository;
    private readonly CandidatFactory _candidatFactory;
    private readonly IRecruteurRepository _recruteurRepository;

    public CommandBusFactory(IEntretienRepository entretienRepository, IEmailService emailService, MessageBus messsageBus, ICandidatRepository candidatRepository, CandidatFactory candidatFactory, IRecruteurRepository recruteurRepository)
    {
        this._entretienRepository = entretienRepository;
        this._emailService = emailService;
        this._messsageBus = messsageBus;
        this._candidatRepository = candidatRepository;
        this._candidatFactory = candidatFactory;
        this._recruteurRepository = recruteurRepository;
    }

    protected List<ICommandHandler<ICommand, CommandResponse>> GetCommandHandlers()
    {
        return new List<ICommandHandler<ICommand, CommandResponse>>
        {
            new PlanifierEntretienCommandHandler(_entretienRepository, _emailService, _messsageBus) as ICommandHandler<ICommand, CommandResponse>,
            new CreerCandidatCommandHandler(_candidatRepository, _candidatFactory) as ICommandHandler<ICommand, CommandResponse>,
            new ValiderEntretienCommandHandler(_entretienRepository) as ICommandHandler<ICommand, CommandResponse>,
            new CreerRecruteurCommandHandler(_recruteurRepository, _messsageBus) as ICommandHandler<ICommand, CommandResponse>
        };
    }

    protected List<IEventHandler> GetEventHandlers()
    {
        return new List<IEventHandler>();
    }

    public ICommandBus Build()
    {
        CommandBusDispatcher commandBusDispatcher = BuildCommandBusDispatcher();

        IEventBus eventBus = BuildEventBus();

        CommandBusLogger commandBusLogger = new CommandBusLogger(commandBusDispatcher);

        return new EventBusDispatcherCommandBus(commandBusLogger, eventBus);
    }

    private IEventBus BuildEventBus()
    {
        EventBusFactory eventBusFactory = new EventBusFactory(GetEventHandlers());
        return eventBusFactory.Build();
    }

    private CommandBusDispatcher BuildCommandBusDispatcher()
    {
        var commandHandlers = GetCommandHandlers();
        return new CommandBusDispatcher(commandHandlers);
    }
}
