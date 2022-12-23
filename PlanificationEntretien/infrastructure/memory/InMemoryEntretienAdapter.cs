using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.infrastructure.memory;

public class InMemoryEntretienAdapter : IEntretienPort
{
    private Dictionary<Candidat, InMemoryEntretien> _entretiens = new();

    public Entretien FindByCandidat(Candidat candidat)
    {
        InMemoryEntretien value;
        _entretiens.TryGetValue(candidat, out value);
        if (value == null)
        {
            return null;
        }
        return ToEntretien(value);
    }

    private static Entretien ToEntretien(InMemoryEntretien? value)
    {
        return new Entretien( InMemoryCandidatAdapter.ToCandidat(value.Candidat), InMemoryRecruteurAdapter.ToRecruteur(value.Recruteur), value.Horaire);
    }

    public void Save(Entretien entretien)
    {
        _entretiens.TryAdd(entretien.Candidat, new InMemoryEntretien(InMemoryCandidatAdapter.ToInMemoryCandidat(entretien.Candidat),
            InMemoryRecruteurAdapter.ToInMemoryRecruteur(entretien.Recruteur), entretien.Horaire));
    }

    public IEnumerable<Entretien> FindAll()
    {
        return _entretiens.Values.Select(e => ToEntretien(e)).ToList();
    }
}