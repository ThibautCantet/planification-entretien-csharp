using System;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.entretien.infrastructure.controller;

public record EntretienResponse(string EmailCandiat, string EmailRecruteur, DateTime dateTime, Status Status);