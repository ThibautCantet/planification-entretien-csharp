using PlanificationEntretien.domain;

namespace PlanificationEntretien.candidat.domain_service;

public record Result<T>(Event Event, T Value);