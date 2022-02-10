using System;

namespace PlanificationEntretien
{
    public class EntretienService
    {
        private readonly IEntretienRepository _entretienRepository;
        private readonly IEmailService _emailService;

        public EntretienService(IEntretienRepository entretienRepository, IEmailService emailService)
        {
            _entretienRepository = entretienRepository;
            _emailService = emailService;
        }

        public Entretien planifier(Candidat candidat, Recruteur recruteur, DateTime disponibiliteCandidat, DateTime disponibiliteRecruteur)
        {
            var entretien = new Entretien(disponibiliteCandidat, candidat.Email, recruteur.Email);
            _emailService.SendToCandidat(candidat.Email);
            _emailService.SendToRecruteur(recruteur.Email);
            
            return _entretienRepository.Save(entretien);
        }
    }
}