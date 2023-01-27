using System;

namespace PlanificationEntretien.infrastructure.repository;

public record InMemoryEntretien(
    int Id,
    InMemoryCandidat Candidat,
    InMemoryRecruteur Recruteur,
    DateTime Horaire)
{
}