using System;

namespace PlanificationEntretien
{
    public class EntretienService
    {
        public EntretienService(IEntretienRepository entretienRepository, IEmailService emailService)
        {
        }

        public Entretien planifier(Candidat candidat, Recruteur recruteur, DateTime disponibiliteCandidat, DateTime disponibiliteRecruteur)
        {
            return null;
        }
    }
}