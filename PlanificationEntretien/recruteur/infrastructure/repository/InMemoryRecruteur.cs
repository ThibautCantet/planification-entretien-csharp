namespace PlanificationEntretien.recruteur.infrastructure.repository;

public record InMemoryRecruteur(int Id, string Language, string Email, int? ExperienceEnAnnees, bool EstDisponible)
{
}