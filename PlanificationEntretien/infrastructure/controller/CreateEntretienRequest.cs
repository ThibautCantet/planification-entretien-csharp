using System;

namespace PlanificationEntretien.infrastructure.controller;

public class CreateEntretienRequest
{
    public CreateEntretienRequest(string? emailCandidat, string? emailRecruteur, DateTime disponibiliteCandidat, DateTime disponibiliteRecruteur)
    {
        EmailCandidat = emailCandidat;
        EmailRecruteur = emailRecruteur;
        DisponibiliteCandidat = disponibiliteCandidat;
        DisponibiliteRecruteur = disponibiliteRecruteur;
    }

    public DateTime DisponibiliteCandidat { get; }
    public DateTime DisponibiliteRecruteur { get; }
    public string? EmailCandidat { get; }
    public string? EmailRecruteur { get; }
 
}
