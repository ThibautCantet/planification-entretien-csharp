using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.recruteur.application_service.application;

namespace PlanificationEntretien.recruteur.infrastructure.repository;

public class InMemoryRecruteurDao : IRecruteurDao
{
    private readonly Dictionary<string,InMemoryRecruteur> _recruteurs;

    public InMemoryRecruteurDao(InMemoryRecruteurRepository inMemoryRecruteurRepository)
    {
        _recruteurs = inMemoryRecruteurRepository.Recruteurs;
    }

    public List<IRecruteurDetail> Find10AnsExperience()
    {
        return new List<IRecruteurDetail>(_recruteurs.Values
            .Where(r => r.ExperienceEnAnnees >= 10)
            .ToList());
    }
}