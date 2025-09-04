using Candidat.domain;

namespace Candidat.infrastructure.repository;

public class InMemoryCandidatRepository : ICandidatRepository
{
    private readonly Dictionary<string, InMemoryCandidat> _candidats = new();

    public domain.Candidat FindById(int id)
    {
        var inMemoryCandidat = _candidats.Values.FirstOrDefault(candidat => candidat.id == id);
        return (inMemoryCandidat != null ? ToCandidat(inMemoryCandidat) : null)!;
    }

    public domain.Candidat FindByEmail(string email)
    {
        _candidats.TryGetValue(email, out var value);
        if (value == null)
        {
            return null;
        }
        return ToCandidat(value);
    }

    public int Save(domain.Candidat candidat)
    {
        var newId = _candidats.Count + 1;
        _candidats.TryAdd(candidat.Email, ToInMemoryCandidat(candidat,newId));

        return newId;
    }

    private static InMemoryCandidat ToInMemoryCandidat(domain.Candidat candidat, int idCandidat)
    {
        return new InMemoryCandidat(idCandidat, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees);
    }

    internal static domain.Candidat ToCandidat(InMemoryCandidat? value)
    {
        return new domain.Candidat(value!.id, value.Language, value.Email, value.ExperienceEnAnnees);
    }
   
}