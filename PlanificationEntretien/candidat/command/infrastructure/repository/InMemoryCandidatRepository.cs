using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.candidat.domain;
using entretienCandidat = PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.candidat.infrastructure.repository;

public class InMemoryCandidatRepository : ICandidatRepository
{
    private Dictionary<string, InMemoryCandidat> _candidats = new();

    public Candidat FindById(int id)
    {
        var inMemoryCandidat = _candidats.Values.FirstOrDefault(candidat => candidat.id == id);
        return inMemoryCandidat != null ? ToCandidat(inMemoryCandidat) : null;
    }

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

    public int Save(Candidat candidat)
    {
        _candidats.TryAdd(candidat.Email, ToInMemoryCandidat(candidat));

        return candidat.Id;
    }

    public int Next()
    {
        return _candidats.Count + 1;
    }

    internal static InMemoryCandidat ToInMemoryCandidat(Candidat candidat)
    {
        return new InMemoryCandidat(candidat.Id, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees);
    }
    
    internal static InMemoryCandidat ToInMemoryEntretienCandidat(entretienCandidat.Candidat candidat)
    {
        return new InMemoryCandidat(candidat.Id, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees);
    }

    internal static Candidat ToCandidat(InMemoryCandidat? value)
    {
        return new Candidat(value.id, value.Language, value.Email, value.ExperienceEnAnnees);
    }
    
    internal static entretienCandidat.Candidat ToEntretienCandidat(InMemoryCandidat? value)
    {
        return new entretienCandidat.Candidat(value.id, value.Language, value.Email, value.ExperienceEnAnnees);
    }
}