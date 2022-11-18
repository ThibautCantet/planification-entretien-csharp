using System;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.infrastructure.memory;

public record InMemoryEntretien()
{
    
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