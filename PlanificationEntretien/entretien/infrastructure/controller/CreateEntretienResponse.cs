using System;

namespace PlanificationEntretien.entretien.infrastructure.controller;

public record CreateEntretienResponse(int EntretienId, string EmailCandidat, string EmailRecruteur, DateTime Horaire) {
}