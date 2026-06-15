using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.Entretien.Domain;
using PlanificationEntretien.Candidat.Infrastructure.Repository;
using PlanificationEntretien.Recruteur.Infrastructure.Repository;

namespace PlanificationEntretien.entretien.Infrastructure.Repository;

public class InMemoryEntretienRepository : IEntretienRepository
{
    private Dictionary<Entretien.Domain.Candidat, InMemoryEntretien> _entretiens = new();

    public Entretien.Domain.Entretien FindById(int id)
    {
        var inMemoryEntretien = _entretiens.Values.FirstOrDefault(entretien => entretien.Id == id);
        return inMemoryEntretien != null ? ToEntretien(inMemoryEntretien) : null;
    }

    public Entretien.Domain.Entretien FindByCandidat(string candidatEmail)
    {
        InMemoryEntretien value = _entretiens.Values.FirstOrDefault(e => e.Candidat.Email == candidatEmail);
        if (value == null)
        {
            return null;
        }

        return ToEntretien(value);
    }

    private static Entretien.Domain.Entretien ToEntretien(InMemoryEntretien? value)
    {
        return Entretien.Domain.Entretien.of(value.Id, 
            InMemoryCandidatRepository.ToEntretienCandidat(value.Candidat),
            InMemoryRecruteurRepository.ToEntretienRecruteur(value.Recruteur), 
            value.Horaire,
            value.Status);
    }

    public int Save(Entretien.Domain.Entretien entretien)
    {
        if (_entretiens.ContainsKey(entretien.Candidat))
        {
            _entretiens.TryAdd(entretien.Candidat,
                toInMemoryEntretien(entretien));
            return entretien.Id;
        }
        else
        {
            var newId = _entretiens.Count + 1;
            _entretiens.TryAdd(entretien.Candidat,
                toInMemoryEntretien(entretien, newId));
            return newId;
        }
    }

    private static InMemoryEntretien toInMemoryEntretien(Entretien.Domain.Entretien entretien)
    {
        return toInMemoryEntretien(entretien, entretien.Id);
    }

    private static InMemoryEntretien toInMemoryEntretien(Entretien.Domain.Entretien entretien, int newId)
    {
        return new InMemoryEntretien(newId,
            InMemoryCandidatRepository.ToInMemoryEntretienCandidat(entretien.Candidat),
            InMemoryRecruteurRepository.ToInMemoryEntretienRecruteur(entretien.Recruteur), 
            entretien.Horaire,
            entretien.Status);
    }

    public IEnumerable<Entretien.Domain.Entretien> FindAll()
    {
        return _entretiens.Values.Select(e => ToEntretien(e)).ToList();
    }
}