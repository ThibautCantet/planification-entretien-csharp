using System;

namespace PlanificationEntretien.domain
{
    public interface IEmailService
    {
        void EnvoyerUnEmailDeConfirmationAuCandidat(String email, HoraireEntretien horaire);

        void EnvoyerUnEmailDeConfirmationAuRecruteur(String email, HoraireEntretien horaire);
    }
}