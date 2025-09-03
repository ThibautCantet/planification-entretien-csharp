using System;
using PlanificationEntretien.domain.entretien;

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

    public int Execute(CandidatEvalué candidatEvalué, DateTime disponibiliteDuCandidat,
        RecruteurAssigné recruteurAssigné, DateTime disponibiliteDuRecruteur)
    {
        var entretien = new Entretien(candidatEvalué, recruteurAssigné);
        if (entretien.Planifier(disponibiliteDuCandidat, disponibiliteDuRecruteur))
        {
            var entretienId = _entretienRepository.Save(entretien);
            _emailService.EnvoyerUnEmailDeConfirmationAuCandidat(candidatEvalué.Email, disponibiliteDuRecruteur);
            _emailService.EnvoyerUnEmailDeConfirmationAuRecruteur(recruteurAssigné.Email, disponibiliteDuRecruteur);
            return entretienId;
        }

        return -1;
    }
}