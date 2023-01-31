using System;
using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.infrastructure.controller;

public record EntretienResponse(string EmailCandiat, string EmailRecruteur, DateTime dateTime, Status Status);