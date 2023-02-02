namespace PlanificationEntretien.candidat.infrastructure.repository;

public record InMemoryCandidat(int id,
    string Language,
    string Email,
    int ExperienceEnAnnees);