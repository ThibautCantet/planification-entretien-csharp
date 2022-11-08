using System.Collections.Generic;
using PlanificationEntretien.model;

namespace PlanificationEntretien.memory;

public class InMemoryCandidatRepository : ICandidatRepository
{
    private Dictionary<string, Candidat> _candidats = new();

    public Candidat FindByEmail(string email)
    {
        Candidat value;
        _candidats.TryGetValue(email, out value);
        return value;
    }

    public void Save(Candidat candidat)
    {
        _candidats.Add(candidat.Email, candidat);
    }
}