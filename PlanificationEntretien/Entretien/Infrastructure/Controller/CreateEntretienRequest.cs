using System;

namespace PlanificationEntretien.Entretien.Infrastructure.Controller;

public record CreateEntretienRequest(int IdCandidat, int IdRecruteur, DateTime DisponibiliteCandidat, DateTime DisponibiliteRecruteur)
{
}
