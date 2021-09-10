using System;
using PlanificationEntretien.domain;

namespace Planification
{
    public class PlanifierEntretien
    {
        private readonly EntretienRepository _entretienRepository;
        private readonly IEmailService _emailService;

        public PlanifierEntretien(EntretienRepository entretienRepository, IEmailService emailService)
        {
            _entretienRepository = entretienRepository;
            _emailService = emailService;
        }

        public void Execute(Candidat candidat, Disponibilite disponibiliteDuCandidat, Recruteur recruteur, DateTime dateDeDisponibiliteDuRecruteur)
        {
            HoraireEntretien horaireEntretien = new HoraireEntretien(disponibiliteDuCandidat.Horaire);
            _entretienRepository.Save(new Entretien(candidat, recruteur, horaireEntretien));
            _emailService.EnvoyerUnEmailDeConfirmationAuCandidat(candidat.Email, horaireEntretien);
            _emailService.EnvoyerUnEmailDeConfirmationAuRecruteur(recruteur.Email, horaireEntretien);
        }
    }
}