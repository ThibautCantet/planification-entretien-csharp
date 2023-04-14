using System;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.entretien.command.domain;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.entretien.application_service;

public class AnnulerEntretienCommandHandler : ICommandHandler<AnnulerEntretienCommand, CommandResponse>
{
    private readonly IEntretienRepository _entretienRepository;

    public AnnulerEntretienCommandHandler(IEntretienRepository entretienRepository)
    {
        _entretienRepository = entretienRepository;
    }

    public CommandResponse Handle(AnnulerEntretienCommand command)
    {
        var entretien = _entretienRepository.FindById(command.Id);
        if (entretien != null)
        {
            entretien.Annuler();
            _entretienRepository.Save(entretien);
            return new CommandResponse(new EntretienAnnulé());
        }

        return new CommandResponse(new EntretienNonAnnulé());
    }

    public Type ListenTo()
    {
        return typeof(AnnulerEntretienCommand);
    }
}