namespace PlanificationEntretien.candidat.infrastructure.controller;

public record CreateCandidatRequest(string Language, string Email, int? Xp) 
{
}