namespace PlanificationEntretien.Candidat.Infrastructure.Controller;

public record CreateCandidatRequest(string Language, string Email, int? Xp) 
{
}