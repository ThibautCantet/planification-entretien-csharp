using System;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.recruteur.application_service;

public class CreerRecruteurCommandHandler : ICommandHandler<CreerRecruteurCommand, CommandResponse>
{
    private readonly IRecruteurRepository _recruteurRepository;

    public CreerRecruteurCommandHandler(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    public CommandResponse Handle(CreerRecruteurCommand command)
    {
        try
        {
            var recruteur = new Recruteur(command.Language,
                command.Email,
                command.ExperienceEnAnnees);
            
            var savedRecruteurId = _recruteurRepository.Save(recruteur);

            var recruteurCrée = new RecruteurCrée(savedRecruteurId,
                command.Language,
                command.ExperienceEnAnnees.Value,
                command.Email
            );

            return new CommandResponse(recruteurCrée);
        }
        catch (ArgumentException)
        {
            return new CommandResponse(new RecruteurNonCrée());
        }
    }

    public Type ListenTo()
    {
        return typeof(CreerRecruteurCommand);
    }
}