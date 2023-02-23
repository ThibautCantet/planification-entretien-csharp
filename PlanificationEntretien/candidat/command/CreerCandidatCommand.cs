namespace PlanificationEntretien.candidat.application_service;

public record CreerCandidatCommand(string Language, string Email, int? ExperienceEnAnnees);