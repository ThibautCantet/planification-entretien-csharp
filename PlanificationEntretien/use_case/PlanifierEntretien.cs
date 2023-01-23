using System;
using PlanificationEntretien.domain;
using PlanificationEntretien.email;

namespace PlanificationEntretien.use_case;

public class PlanifierEntretien
{
    private readonly IEntretienRepository _entretienRepository;
    private readonly IEmailService _emailService;

    public PlanifierEntretien(IEntretienRepository entretienRepository, IEmailService emailService)
    {
        _entretienRepository = entretienRepository;
        _emailService = emailService;
    }

    public Boolean Execute(Candidat candidat, DateTime disponibiliteDuCandidat,
        Recruteur recruteur, DateTime dateDeDisponibiliteDuRecruteur)
    {
        if (candidat.Language.Equals(recruteur.Language)
            && candidat.ExperienceEnAnnees < recruteur.ExperienceEnAnnees
            && disponibiliteDuCandidat.Equals(dateDeDisponibiliteDuRecruteur))
        {
            Entretien entretien = new Entretien(candidat, recruteur, dateDeDisponibiliteDuRecruteur);
            _entretienRepository.Save(entretien);
            _emailService.EnvoyerUnEmailDeConfirmationAuCandidat(candidat.Email, dateDeDisponibiliteDuRecruteur);
            _emailService.EnvoyerUnEmailDeConfirmationAuRecruteur(recruteur.Email, dateDeDisponibiliteDuRecruteur);
            return true;
        }

        return false;
    }
}