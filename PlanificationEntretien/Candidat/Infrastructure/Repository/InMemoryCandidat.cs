namespace PlanificationEntretien.Candidat.Infrastructure.Repository;

public record InMemoryCandidat(int id,
    string Language,
    string Email,
    int ExperienceEnAnnees);