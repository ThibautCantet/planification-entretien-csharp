using System.Collections.Generic;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.infrastructure.memory;

public class InMemoryCandidatAdapter : ICandidatPort
{
    private Dictionary<string, InMemoryCandidat> _candidats = new();

    public Candidat FindByEmail(string email)
    {
        InMemoryCandidat value;
        _candidats.TryGetValue(email, out value);
        if (value == null)
        {
            return null;
        }
        return ToCandidat(value);
    }

    public void Save(Candidat candidat)
    {
        _candidats.TryAdd(candidat.Email, ToInMemoryCandidat(candidat));
    }

    internal static InMemoryCandidat ToInMemoryCandidat(Candidat candidat)
    {
        return new InMemoryCandidat(candidat.Language, candidat.Email, candidat.ExperienceEnAnnees);
    }

    internal static Candidat ToCandidat(InMemoryCandidat? value)
    {
        return new Candidat(value.Language, value.Email, value.ExperienceEnAnnees);
    }
}