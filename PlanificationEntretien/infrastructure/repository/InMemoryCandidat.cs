namespace PlanificationEntretien.infrastructure.repository;

public record InMemoryCandidat(int id,
    string Language,
    string Email,
    int ExperienceEnAnnees) { 
}