using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.Candidat.Domain;
using entretienCandidat = PlanificationEntretien.Entretien.Domain;

namespace PlanificationEntretien.Candidat.Infrastructure.Repository;

public class InMemoryCandidatRepository : ICandidatRepository
{
    private Dictionary<string, InMemoryCandidat> _candidats = new();

    public Domain.Candidat FindById(int id)
    {
        var inMemoryCandidat = _candidats.Values.FirstOrDefault(candidat => candidat.id == id);
        return inMemoryCandidat != null ? ToCandidat(inMemoryCandidat) : null;
    }

    public Domain.Candidat FindByEmail(string email)
    {
        InMemoryCandidat value;
        _candidats.TryGetValue(email, out value);
        if (value == null)
        {
            return null;
        }
        return ToCandidat(value);
    }

    public int Save(Domain.Candidat candidat)
    {
        _candidats.TryAdd(candidat.Email, ToInMemoryCandidat(candidat));

        return candidat.Id;
    }

    public int Next()
    {
        return _candidats.Count + 1;
    }

    internal static InMemoryCandidat ToInMemoryCandidat(Domain.Candidat candidat)
    {
        return new InMemoryCandidat(candidat.Id, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees);
    }
    
    internal static InMemoryCandidat ToInMemoryEntretienCandidat(entretienCandidat.Candidat candidat)
    {
        return new InMemoryCandidat(candidat.Id, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees);
    }

    internal static Domain.Candidat ToCandidat(InMemoryCandidat? value)
    {
        return new Domain.Candidat(value.id, value.Language, value.Email, value.ExperienceEnAnnees);
    }
    
    internal static entretienCandidat.Candidat ToEntretienCandidat(InMemoryCandidat? value)
    {
        return new entretienCandidat.Candidat(value.id, value.Language, value.Email, value.ExperienceEnAnnees);
    }
}