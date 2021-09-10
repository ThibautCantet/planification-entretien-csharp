using System;
using PlanificationEntretien.domain;
using PlanificationEntretien.Tests;
using IEmailService = PlanificationEntretien.domain.IEmailService;

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

        public ResultatPlanificationEntretien Execute(Candidat candidat, Disponibilite disponibiliteDuCandidat,
            Recruteur recruteur, DateTime dateDeDisponibiliteDuRecruteur)
        {
            HoraireEntretien horaireEntretien = new HoraireEntretien(disponibiliteDuCandidat.Horaire);
            
            Entretien entretien = new Entretien(candidat, recruteur);
            ResultatPlanificationEntretien resultatPlanificationEntretien = entretien.Planifier(disponibiliteDuCandidat, dateDeDisponibiliteDuRecruteur);

            if (resultatPlanificationEntretien.GetType().Equals(typeof(EntretienPlanifie)))
            {
                _entretienRepository.Save(entretien);
                _emailService.EnvoyerUnEmailDeConfirmationAuCandidat(candidat.Email, horaireEntretien);
                _emailService.EnvoyerUnEmailDeConfirmationAuRecruteur(recruteur.Email, horaireEntretien);
            }

            return resultatPlanificationEntretien;
        }
    }
}