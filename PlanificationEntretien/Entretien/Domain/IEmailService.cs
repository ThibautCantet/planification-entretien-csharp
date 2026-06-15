using System;

namespace PlanificationEntretien.Entretien.Domain;

public interface IEmailService
{
    void EnvoyerUnEmailDeConfirmationAuCandidat(string email, DateTime horaire);

    void EnvoyerUnEmailDeConfirmationAuRecruteur(string email, DateTime horaire);
}