using System;

namespace PlanificationEntretien.infrastructure.controller;

public record CreateEntretienResponse(string EmailCandidat, string EmailRecruteur, DateTime Horaire) {
}
