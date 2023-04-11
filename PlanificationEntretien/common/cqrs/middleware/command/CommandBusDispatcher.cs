using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.candidat.application_service;
using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.candidat.domain_service;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class CommandBusDispatcher : ICommandBus
{
    private readonly ICandidatRepository _candidatRepository;
    private readonly CandidatFactory _candidatFactory;

    public CommandBusDispatcher(ICandidatRepository candidatRepository, CandidatFactory candidatFactory)
    {
        this._candidatRepository = candidatRepository;
        this._candidatFactory = candidatFactory;
    }

    public CommandResponse Dispatch(ICommand command)
    {
        if (command is CreerCandidatCommand)
        {
            return new CreerCandidatCommandHandler(_candidatRepository, _candidatFactory).Handle(command as CreerCandidatCommand);
        }

        throw new UnmatchedCommandHandlerException(command);
    }
}
