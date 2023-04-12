using System;
using System.Collections.Generic;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.application_service;
using PlanificationEntretien.domain;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.recruteur.application_service;

public class CreerRecruteurCommandHandler : ICommandHandler<CreerRecruteurCommand, CommandResponse>
{
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly MessageBus _messageBus;

    public CreerRecruteurCommandHandler(IRecruteurRepository recruteurRepository, MessageBus messageBus)
    {
        _recruteurRepository = recruteurRepository;
        _messageBus = messageBus;
    }

    public CommandResponse Handle(CreerRecruteurCommand command)
    {
        try
        {
            var recruteur = new Recruteur(command.Language,
                command.Email,
                command.ExperienceEnAnnees);
            
            var savedRecruteurId = _recruteurRepository.Save(recruteur);
            
            _messageBus.Send(new RecruteurCrée(savedRecruteurId,
                command.Language,
                command.ExperienceEnAnnees.Value,
                command.Email
            ));

            //return savedRecruteurId;
        }
        catch (ArgumentException)
        {
        }
        return new CommandResponse(new List<Event>());
    }

    public Type ListenTo()
    {
        return typeof(CreerRecruteurCommand);
    }
}