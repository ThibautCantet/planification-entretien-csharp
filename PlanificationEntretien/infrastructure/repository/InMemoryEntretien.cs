using System;

namespace PlanificationEntretien.infrastructure.repository;

public record InMemoryEntretien()
{
    
    public int Id { get; }
    public InMemoryCandidat Candidat { get; }
    public InMemoryRecruteur Recruteur { get; }
    public DateTime Horaire { get; }

    public InMemoryEntretien(InMemoryCandidat candidat, InMemoryRecruteur recruteur, DateTime horaire) : this()
    {
        Candidat = candidat;
        Recruteur = recruteur;
        Horaire = horaire;
    }
}