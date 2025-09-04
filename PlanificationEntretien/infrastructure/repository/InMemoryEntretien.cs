using System;
using Candidat.infrastructure.repository;
using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.infrastructure.repository;

public record InMemoryEntretien(
    int Id,
    InMemoryCandidat Candidat,
    InMemoryRecruteur Recruteur,
    DateTime Horaire,
    Status Status);