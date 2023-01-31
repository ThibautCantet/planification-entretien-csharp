using System;
using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.infrastructure.repository;

public record InMemoryEntretien(
    int Id,
    InMemoryCandidat Candidat,
    InMemoryRecruteur Recruteur,
    DateTime Horaire,
    Status Status)
{
}