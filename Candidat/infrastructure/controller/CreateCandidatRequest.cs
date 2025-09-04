namespace Candidat.infrastructure.controller;

public record CreateCandidatRequest(string Language, string Email, int? Xp)
{
}