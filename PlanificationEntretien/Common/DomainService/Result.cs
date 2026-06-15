using PlanificationEntretien.Common.Domain;

namespace PlanificationEntretien.Common.domain_service;

public record Result<T>(Event Event, T Value);