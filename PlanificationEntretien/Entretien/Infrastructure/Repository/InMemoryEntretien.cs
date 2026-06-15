using System;
using PlanificationEntretien.Entretien.Domain;
using PlanificationEntretien.Candidat.Infrastructure.Repository;
using PlanificationEntretien.Recruteur.Infrastructure.Repository;

namespace PlanificationEntretien.entretien.Infrastructure.Repository;

public record InMemoryEntretien(
    int Id,
    InMemoryCandidat Candidat,
    InMemoryRecruteur Recruteur,
    DateTime Horaire,
    Status Status)
{
}