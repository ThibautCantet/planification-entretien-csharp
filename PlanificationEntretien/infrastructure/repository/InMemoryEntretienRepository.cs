using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.infrastructure.repository;

public class InMemoryEntretienRepository : IEntretienRepository
{
    private Dictionary<Candidat, InMemoryEntretien> _entretiens = new();

    public Entretien FindById(int id)
    {
        var inMemoryEntretien = _entretiens.Values.FirstOrDefault(entretien => entretien.Id == id);
        return inMemoryEntretien != null ? ToEntretien(inMemoryEntretien) : null;
    }

    public Entretien FindByCandidat(string candidatEmail)
    {
        InMemoryEntretien value = _entretiens.Values.FirstOrDefault(e => e.Candidat.Email == candidatEmail);
        if (value == null)
        {
            return null;
        }

        return ToEntretien(value);
    }

    private static Entretien ToEntretien(InMemoryEntretien? value)
    {
        return Entretien.of(value.Id, 
            InMemoryCandidatRepository.ToEntretienCandidat(value.Candidat),
            InMemoryRecruteurRepository.ToEntretienRecruteur(value.Recruteur), 
            value.Horaire,
            value.Status);
    }

    public int Save(Entretien entretien)
    {
        var newId = _entretiens.Count + 1;
        _entretiens.TryAdd(entretien.Candidat,
            new InMemoryEntretien(newId,
                InMemoryCandidatRepository.ToInMemoryEntretienCandidat(entretien.Candidat),
                InMemoryRecruteurRepository.ToInMemoryEntretienRecruteur(entretien.Recruteur), 
                entretien.Horaire,
                entretien.Status));
        return newId;
    }

    public IEnumerable<Entretien> FindAll()
    {
        return _entretiens.Values.Select(e => ToEntretien(e)).ToList();
    }
}