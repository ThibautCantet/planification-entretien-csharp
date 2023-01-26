using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.infrastructure.repository;

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
        var newId = _candidats.Count + 1;
        _candidats.TryAdd(candidat.Email, ToInMemoryCandidat(candidat,newId));

        return newId;
    }

    internal static InMemoryCandidat ToInMemoryCandidat(Candidat candidat)
    {
        return new InMemoryCandidat(candidat.Id, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees);
    }
    internal static InMemoryCandidat ToInMemoryCandidat(Candidat candidat, int idCandidat)
    {
        return new InMemoryCandidat(idCandidat, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees);
    }

    internal static Candidat ToCandidat(InMemoryCandidat? value)
    {
        return new Candidat(value.id, value.Language, value.Email, value.ExperienceEnAnnees);
    }
}