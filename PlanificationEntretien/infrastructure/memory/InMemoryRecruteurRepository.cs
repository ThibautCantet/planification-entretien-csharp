using System.Collections.Generic;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.infrastructure.memory;

public class InMemoryRecruteurRepository : IRecruteurRepository
{
    private Dictionary<string, Recruteur> _candidats = new();

    public Recruteur FindByEmail(string email)
    {
        Recruteur value;
        _candidats.TryGetValue(email, out value);
        return value;
    }

    public void Save(Recruteur candidat)
    {
        _candidats.Add(candidat.Email, candidat);
    }
}