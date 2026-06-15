using System;

namespace PlanificationEntretien.Entretien.Infrastructure.Controller;

public record CreateEntretienResponse(int EntretienId, string EmailCandidat, string EmailRecruteur, DateTime Horaire) {
}
