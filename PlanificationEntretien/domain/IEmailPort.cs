using System;

namespace PlanificationEntretien.email;

public interface IEmailPort
{
    void EnvoyerUnEmailDeConfirmationAuCandidat(string email, DateTime horaire);

    void EnvoyerUnEmailDeConfirmationAuRecruteur(string email, DateTime horaire);
}