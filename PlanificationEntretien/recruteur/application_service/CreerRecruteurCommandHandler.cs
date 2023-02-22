using System;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.recruteur.application_service;

public class CreerRecruteurCommandHandler
{
    private readonly IRecruteurRepository _recruteurRepository;

    public CreerRecruteurCommandHandler(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    public int Handle(CreerRecruteurCommand command)
    {
        try
        {
            var recruteur = new Recruteur(command.Language,
                command.Email,
                command.ExperienceEnAnnees);
            return _recruteurRepository.Save(recruteur);
        }
        catch (ArgumentException)
        {
            return -1;
        }
    }
}