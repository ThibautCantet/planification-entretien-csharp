using System;
using Candidat.domain;
using PlanificationEntretien.domain.entretien;
using PlanificationEntretien.domain.recruteur;
using Shared;

namespace PlanificationEntretien.use_case;

public class PlanifierEntretien
{
    private readonly IEntretienRepository _entretienRepository;
    private readonly IEmailService _emailService;
    private readonly ICandidatRepository _candidatRepository;
    private readonly IRecruteurRepository _recruteurRepository;

    public PlanifierEntretien(IEntretienRepository entretienRepository,
        ICandidatRepository candidatRepository,
        IRecruteurRepository recruteurRepository,
        IEmailService emailService)
    {
        _entretienRepository = entretienRepository;
        _candidatRepository = candidatRepository;
        _recruteurRepository = recruteurRepository;
        _emailService = emailService;
    }

    public Event Execute(int candidatEvaluéId, DateTime disponibiliteDuCandidat,
        int recruteurAssignéId, DateTime disponibiliteDuRecruteur)
    {

        var candidatEvalué = _candidatRepository.FindById(candidatEvaluéId);
        var recruteurAssigné = _recruteurRepository.FindById(recruteurAssignéId);

        var entretien = new Entretien(
            new CandidatEvalué(candidatEvalué.Id, candidatEvalué.Language, candidatEvalué.Email, candidatEvalué.ExperienceEnAnnees),
            new RecruteurAssigné(recruteurAssigné.Id, recruteurAssigné.Language, recruteurAssigné.Email, recruteurAssigné.ExperienceEnAnnees));

        if (entretien.Planifier(disponibiliteDuCandidat, disponibiliteDuRecruteur))
        {
            var entretienId = _entretienRepository.Save(entretien);
            _emailService.EnvoyerUnEmailDeConfirmationAuCandidat(candidatEvalué.Email, disponibiliteDuRecruteur);
            _emailService.EnvoyerUnEmailDeConfirmationAuRecruteur(recruteurAssigné.Email, disponibiliteDuRecruteur);
            return new EntretienPlanifie(entretienId, candidatEvalué.Email, recruteurAssigné.Email);
        }

        return new PlanificationEntretienEchoué(entretien);
    }
}