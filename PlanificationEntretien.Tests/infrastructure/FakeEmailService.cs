using PlanificationEntretien.domain;

namespace PlanificationEntretien.Tests
{
    public class FakeEmailService : IEmailService
    {
        private bool _unEmailDeConfirmationAEteEnvoyeAuCandidat;
        private bool _unEmailDeConfirmationAEteEnvoyeAuRecruteur;

        public bool UnEmailDeConfirmationAEteEnvoyeAuCandidat(string candidatEmail, HoraireEntretien horaire)
        {
            return _unEmailDeConfirmationAEteEnvoyeAuCandidat;
        }

        public bool UnEmailDeConfirmationAEteEnvoyeAuRecruteur(string recruteurEmail, HoraireEntretien horaire)
        {
            return _unEmailDeConfirmationAEteEnvoyeAuRecruteur;
        }

        public void EnvoyerUnEmailDeConfirmationAuCandidat(string email, HoraireEntretien horaire)
        {
            _unEmailDeConfirmationAEteEnvoyeAuCandidat = true;
        }

        public void EnvoyerUnEmailDeConfirmationAuRecruteur(string email, HoraireEntretien horaire)
        {
            _unEmailDeConfirmationAEteEnvoyeAuRecruteur = true;
        }
    }
}