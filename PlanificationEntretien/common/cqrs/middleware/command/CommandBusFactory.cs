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
    private readonly IEntretienRepository entretienRepository;
    private readonly IEmailService emailService;
    private readonly MessageBus messsageBus;
    private readonly ICandidatRepository candidatRepository;
    private readonly CandidatFactory candidatFactory;
    private readonly IRecruteurRepository recruteurRepository;

    protected List<ICommandHandler<ICommand>> GetCommandHandlers()
    {
        return new List<ICommandHandler<ICommand>>
        {
            new PlanifierEntretienCommandHandler(entretienRepository, emailService, messsageBus) as ICommandHandler<ICommand>,
            new CreerCandidatCommandHandler(candidatRepository, candidatFactory) as ICommandHandler<ICommand>,
            new ValiderEntretienCommandHandler(entretienRepository) as ICommandHandler<ICommand>,
            new CreerRecruteurCommandHandler(recruteurRepository, messsageBus) as ICommandHandler<ICommand>
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
