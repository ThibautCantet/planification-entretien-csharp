using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.infrastructure.repository;

public class InMemoryEntretienRepository : IEntretienRepository
{
    private Dictionary<CandidatEvalué, InMemoryEntretien> _entretiens = new();

    public Entretien FindById(int id)
    {
        var inMemoryEntretien = _entretiens.Values.FirstOrDefault(entretien => entretien.Id == id);
        return inMemoryEntretien != null ? ToEntretien(inMemoryEntretien) : null;
    }

    public Entretien FindByCandidat(CandidatEvalué candidat)
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
        return Entretien.of( value.Id, InMemoryCandidatRepository.ToCandidatEvalué(value.Candidat), InMemoryRecruteurRepository.ToRecruteurAssigné(value.Recruteur), value.Horaire);
    }

    public int Save(Entretien entretien)
    {
        var newId = _entretiens.Count + 1;
        _entretiens.TryAdd(entretien.CandidatEvalué, new InMemoryEntretien(newId, InMemoryCandidatRepository.ToInMemoryCandidat(entretien.CandidatEvalué),
            InMemoryRecruteurRepository.ToInMemoryRecruteur(entretien.RecruteurAssigné), entretien.Horaire));
        return newId;
    }

    public IEnumerable<Entretien> FindAll()
    {
        return _entretiens.Values.Select(e => ToEntretien(e)).ToList();
    }
}