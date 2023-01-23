using System;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.use_case;

public class CreerRecruteur
{
    private readonly IRecruteurRepository _recruteurRepository;

    public CreerRecruteur(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    public bool Execute(String language, String email, int? experienceEnAnnees)
    {
        try
        {
            var recruteur = new Recruteur(language,
                email,
                experienceEnAnnees);
            _recruteurRepository.Save(recruteur);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}