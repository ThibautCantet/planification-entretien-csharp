using System;

namespace PlanificationEntretien.infrastructure.controller;

public record CreateEntretienResponse(int EntretienId, string EmailCandidat, string EmailRecruteur, DateTime Horaire) {
}
