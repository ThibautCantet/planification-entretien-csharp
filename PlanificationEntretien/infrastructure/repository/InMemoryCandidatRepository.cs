using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.domain.candidat;
using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.infrastructure.repository;

public class InMemoryCandidatRepository : ICandidatRepository
{
    private readonly Dictionary<string, InMemoryCandidat> _candidats = new();

    public Candidat FindById(int id)
    {
        var inMemoryCandidat = _candidats.Values.FirstOrDefault(candidat => candidat.id == id);
        return (inMemoryCandidat != null ? ToCandidat(inMemoryCandidat) : null)!;
    }

    public Candidat FindByEmail(string email)
    {
        _candidats.TryGetValue(email, out var value);
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

    internal static InMemoryCandidat ToInMemoryCandidat(CandidatEvalué candidat)
    {
        return new InMemoryCandidat(candidat.Id, candidat.Langage, candidat.Email, candidat.ExperienceEnAnnees);
    }

    private static InMemoryCandidat ToInMemoryCandidat(Candidat candidat, int idCandidat)
    {
        return new InMemoryCandidat(idCandidat, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees);
    }

    internal static Candidat ToCandidat(InMemoryCandidat? value)
    {
        return new Candidat(value!.id, value.Language, value.Email, value.ExperienceEnAnnees);
    }
    
    internal static CandidatEvalué ToCandidatEvalué(InMemoryCandidat? value)
    {
        return new CandidatEvalué(value!.id, value.Language, value.Email, value.ExperienceEnAnnees);
    }
}