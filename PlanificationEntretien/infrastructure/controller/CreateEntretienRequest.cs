using System;

namespace PlanificationEntretien.infrastructure.controller;

public record CreateEntretienRequest(int IdCandidat, int IdRecruteur, DateTime DisponibiliteCandidat, DateTime DisponibiliteRecruteur)
{
}
