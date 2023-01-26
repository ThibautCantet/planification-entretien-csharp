using System;

namespace PlanificationEntretien.infrastructure.controller;

public record CreateEntretienRequest(int IdCandidat, string? EmailRecruteur, DateTime DisponibiliteCandidat, DateTime DisponibiliteRecruteur)
{
}
