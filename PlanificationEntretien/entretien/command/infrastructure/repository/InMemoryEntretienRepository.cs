using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.candidat.infrastructure.repository;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.recruteur.infrastructure.repository;

namespace PlanificationEntretien.entretien.infrastructure.repository;

public class InMemoryEntretienRepository : IEntretienRepository
{
    internal Dictionary<Candidat, InMemoryEntretien> Entretiens { get; } = new();

    public Entretien FindById(int id)
    {
        var inMemoryEntretien = Entretiens.Values.FirstOrDefault(entretien => entretien.Id == id);
        return inMemoryEntretien != null ? ToEntretien(inMemoryEntretien) : null;
    }

    public Entretien FindByCandidat(string candidatEmail)
    {
        InMemoryEntretien value = Entretiens.Values.FirstOrDefault(e => e.Candidat.Email == candidatEmail);
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
        if (Entretiens.ContainsKey(entretien.Candidat))
        {
            Entretiens.TryAdd(entretien.Candidat,
                toInMemoryEntretien(entretien));
            return entretien.Id;
        }

        var newId = Entretiens.Count + 1;
        Entretiens.TryAdd(entretien.Candidat,
            toInMemoryEntretien(entretien, newId));
        return newId;
    }

    private static InMemoryEntretien toInMemoryEntretien(Entretien entretien)
    {
        return toInMemoryEntretien(entretien, entretien.Id);
    }

    private static InMemoryEntretien toInMemoryEntretien(Entretien entretien, int newId)
    {
        return new InMemoryEntretien(newId,
            InMemoryCandidatRepository.ToInMemoryEntretienCandidat(entretien.Candidat),
            InMemoryRecruteurRepository.ToInMemoryEntretienRecruteur(entretien.Recruteur), 
            entretien.Horaire,
            entretien.Status);
    }

}