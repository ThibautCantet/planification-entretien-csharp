using System;
using PlanificationEntretien.domain.entretien;
using Shared;

namespace PlanificationEntretien.use_case;

public class PlanifierEntretien
{
    private readonly IEntretienRepository _entretienRepository;
    private readonly IEmailService _emailService;
    private readonly ICandidatEvalueDAO _candidatDao;
    private readonly IRecruteurAssigneDao _recruteurDao;

    public PlanifierEntretien(IEntretienRepository entretienRepository,
        ICandidatEvalueDAO candidatDao,
        IRecruteurAssigneDao recruteurDao,
        IEmailService emailService)
    {
        _entretienRepository = entretienRepository;
        _candidatDao = candidatDao;
        _recruteurDao = recruteurDao;
        _emailService = emailService;
    }

    public Event Execute(int candidatEvaluéId, DateTime disponibiliteDuCandidat,
        int recruteurAssignéId, DateTime disponibiliteDuRecruteur)
    {
        var candidatEvalué = _candidatDao.FindById(candidatEvaluéId);
        var recruteurAssigné = _recruteurDao.FindById(recruteurAssignéId);

        var entretien = new Entretien(candidatEvalué, recruteurAssigné);

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