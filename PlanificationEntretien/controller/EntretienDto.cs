using System;

namespace PlanificationEntretien.controller;

public class EntretienDto
{
    public EntretienDto(string? emailCandidat, string? emailRecruteur, DateTime disponibiliteCandidat, DateTime disponibiliteRecruteur)
    {
        EmailCandidat = emailCandidat;
        EmailRecruteur = emailRecruteur;
        DisponibiliteCandidat = disponibiliteCandidat;
        DisponibiliteRecruteur = disponibiliteRecruteur;
    }

    public DateTime DisponibiliteCandidat { get; init; }
    public DateTime DisponibiliteRecruteur { get; init; }
    public string? EmailCandidat { get; init; }
    public string? EmailRecruteur { get; init; }
 
}
