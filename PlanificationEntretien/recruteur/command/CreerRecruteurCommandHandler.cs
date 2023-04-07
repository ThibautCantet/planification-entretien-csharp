using System;
using PlanificationEntretien.application_service;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.recruteur.application_service;

public class CreerRecruteurCommandHandler
{
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly MessageBus _messageBus;

    public CreerRecruteurCommandHandler(IRecruteurRepository recruteurRepository, MessageBus messageBus)
    {
        _recruteurRepository = recruteurRepository;
        _messageBus = messageBus;
    }

    public int Handle(CreerRecruteurCommand command)
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

            return savedRecruteurId;
        }
        catch (ArgumentException)
        {
            return -1;
        }
    }
}