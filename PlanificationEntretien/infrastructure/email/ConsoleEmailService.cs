using System;
using PlanificationEntretien.domain.entretien;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.infrastructure.email;

public class ConsoleEmailService : IEmailService
{
    public void EnvoyerUnEmailDeConfirmationAuCandidat(string email, DateTime horaire)
    {
        Console.WriteLine($"📧 Email de confirmation envoyé au candidat: {email} pour le {horaire:g}");
    }

    public void EnvoyerUnEmailDeConfirmationAuRecruteur(string email, DateTime horaire)
    {
        Console.WriteLine($"📧 Email de confirmation envoyé au recruteur: {email} pour le {horaire:g}");
    }
}

