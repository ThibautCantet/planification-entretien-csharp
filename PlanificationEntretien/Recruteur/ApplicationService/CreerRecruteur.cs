using System;
using PlanificationEntretien.Recruteur.Domain;

namespace PlanificationEntretien.Recruteur.ApplicationService;

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
            var recruteurId = _recruteurRepository.Next();
            var recruteur = new Domain.Recruteur(recruteurId, language,
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