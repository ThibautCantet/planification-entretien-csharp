using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.candidat.application_service;
using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.candidat.domain_service;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.recruteur.application_service;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class CommandBusDispatcher : ICommandBus
{
    private readonly ICandidatRepository _candidatRepository;
    private readonly CandidatFactory _candidatFactory;
    private readonly IEntretienRepository _entretienRepository;
    private readonly IEmailService _emailService;
    private readonly IRecruteurRepository _recruteurRepository;

    public CommandBusDispatcher(ICandidatRepository candidatRepository, CandidatFactory candidatFactory, IEntretienRepository entretienRepository, IEmailService emailService, IRecruteurRepository recruteurRepository)
    {
        this._candidatRepository = candidatRepository;
        this._candidatFactory = candidatFactory;
        this._entretienRepository = entretienRepository;
        this._emailService = emailService;
        this._recruteurRepository = recruteurRepository;
    }

    public CommandResponse Dispatch(ICommand command)
    {
        if (command is CreerCandidatCommand)
        {
            return new CreerCandidatCommandHandler(_candidatRepository, _candidatFactory).Handle(command as CreerCandidatCommand);
        }
        if (command is PlanifierEntretienCommand)
        {
            return new PlanifierEntretienCommandHandler(_entretienRepository, _emailService).Handle(command as PlanifierEntretienCommand);
        }
        if (command is ValiderEntretienCommand)
        {
            return new ValiderEntretienCommandHandler(_entretienRepository).Handle(command as ValiderEntretienCommand);
        }
        if (command is CreerRecruteurCommand)
        {
            return new CreerRecruteurCommandHandler(_recruteurRepository).Handle(command as CreerRecruteurCommand);
        }

        throw new UnmatchedCommandHandlerException(command);
    }
}
