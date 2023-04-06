using System;

namespace PlanificationEntretien.entretien.infrastructure.controller;

public record CreateEntretienRequest(int IdCandidat, int IdRecruteur, DateTime DisponibiliteCandidat, DateTime DisponibiliteRecruteur)
{
}
