using System;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.candidat.infrastructure.repository;
using PlanificationEntretien.recruteur.infrastructure.repository;

namespace PlanificationEntretien.entretien.infrastructure.repository;

public record InMemoryEntretien(
    int Id,
    InMemoryCandidat Candidat,
    InMemoryRecruteur Recruteur,
    DateTime Horaire,
    Status Status)
{
}