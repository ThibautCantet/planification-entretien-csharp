using System;
using PlanificationEntretien.domain.candidat;
using PlanificationEntretien.domain.entretien;
using PlanificationEntretien.domain.recruteur;

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

    public int Execute(Candidat candidat, DateTime disponibiliteDuCandidat,
        Recruteur recruteur, DateTime disponibiliteDuRecruteur)
    {
        var entretien = new Entretien(candidat, recruteur);
        if (entretien.Planifier(disponibiliteDuCandidat, disponibiliteDuRecruteur))
        {
            var entretienId = _entretienRepository.Save(entretien);
            _emailService.EnvoyerUnEmailDeConfirmationAuCandidat(candidat.Email, disponibiliteDuRecruteur);
            _emailService.EnvoyerUnEmailDeConfirmationAuRecruteur(recruteur.Email, disponibiliteDuRecruteur);
            return entretienId;
        }

        return -1;
    }
}