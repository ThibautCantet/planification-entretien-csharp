using System;

namespace PlanificationEntretien.infrastructure.controller;

public record EntretienResponse(string EmailCandiat, string EmailRecruteur, DateTime Horaire);