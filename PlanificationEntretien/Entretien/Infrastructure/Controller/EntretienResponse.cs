using System;
using PlanificationEntretien.Entretien.Domain;

namespace PlanificationEntretien.Entretien.Infrastructure.Controller;

public record EntretienResponse(string EmailCandiat, string EmailRecruteur, DateTime DateTime, Status Status);