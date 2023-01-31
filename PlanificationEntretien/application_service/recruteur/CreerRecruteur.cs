using System;
using PlanificationEntretien.domain.recruteur;

namespace PlanificationEntretien.application_service.recruteur;

public class CreerRecruteur
{
    private readonly IRecruteurRepository _recruteurRepository;

    public CreerRecruteur(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    public int Execute(String language, String email, int? experienceEnAnnees)
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