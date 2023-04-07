using System.Collections.Generic;
using PlanificationEntretien.recruteur.application_service.application;

namespace PlanificationEntretien.recruteur.infrastructure.repository;

public class InMemoryRecruteurDao : IRecruteurDao
{
    private readonly List<RecruteurDetail> _recruteurs = new();

    public List<RecruteurDetail> Find10AnsExperience()
    {
        return _recruteurs;
    }

    public void AddExperimente(RecruteurDetail recruteur)
    {
        _recruteurs.Add(recruteur);
    }
}