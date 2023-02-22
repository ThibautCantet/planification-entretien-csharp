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

    public int Handle(String language, String email, int? experienceEnAnnees)
    {
        try
        {
            var recruteur = new Recruteur(language,
                email,
                experienceEnAnnees);
            return _recruteurRepository.Save(recruteur);
        }
        catch (ArgumentException)
        {
            return -1;
        }
    }
}