using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.application_service;
using PlanificationEntretien.candidat.application_service;
using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.candidat.domain_service;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class CommandBusDispatcher : ICommandBus
{
    private readonly ICandidatRepository _candidatRepository;
    private readonly CandidatFactory _candidatFactory;
    private readonly IEntretienRepository _entretienRepository;
    private readonly IEmailService _emailService;
    private readonly MessageBus _messsageBus;

    public CommandBusDispatcher(ICandidatRepository candidatRepository, CandidatFactory candidatFactory, IEntretienRepository entretienRepository, IEmailService emailService, MessageBus messsageBus)
    {
        this._candidatRepository = candidatRepository;
        this._candidatFactory = candidatFactory;
        this._entretienRepository = entretienRepository;
        this._emailService = emailService;
        this._messsageBus = messsageBus;
    }

    public CommandResponse Dispatch(ICommand command)
    {
        if (command is CreerCandidatCommand)
        {
            return new CreerCandidatCommandHandler(_candidatRepository, _candidatFactory).Handle(command as CreerCandidatCommand);
        }
        if (command is PlanifierEntretienCommand)
        {
            return new PlanifierEntretienCommandHandler(_entretienRepository, _emailService, _messsageBus).Handle(command as PlanifierEntretienCommand);
        }
        if (command is ValiderEntretienCommand)
        {
            return new ValiderEntretienCommandHandler(_entretienRepository).Handle(command as ValiderEntretienCommand);
        }

        throw new UnmatchedCommandHandlerException(command);
    }
}
