namespace PlanificationEntretien.Recruteur.Infrastructure.Repository;

public record InMemoryRecruteur(int Id, string Language, string Email, int? ExperienceEnAnnees, bool EstDisponible)
{
}