using System;
using PlanificationEntretien.application_service;
using PlanificationEntretien.domain.entretien;
using Shared;

namespace PlanificationEntretien.use_case;

public class PlanifierEntretien
{
    private readonly IEntretienRepository _entretienRepository;
    private readonly IEmailService _emailService;
    private readonly ICandidatEvalueDAO _candidatDao;
    private readonly IRecruteurAssigneDao _recruteurDao;
    private readonly MessageBus _messageBus;

    public PlanifierEntretien(IEntretienRepository entretienRepository,
        ICandidatEvalueDAO candidatDao,
        IRecruteurAssigneDao recruteurDao,
        IEmailService emailService,
        MessageBus messageBus)
    {
        _entretienRepository = entretienRepository;
        _candidatDao = candidatDao;
        _recruteurDao = recruteurDao;
        _emailService = emailService;
        _messageBus = messageBus;
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
            var plannificationReussi = new EntretienPlanifie(entretienId, candidatEvalué.Email, recruteurAssigné.Email);
            _messageBus.Send(plannificationReussi);
            return plannificationReussi;
        }

        var plannificationEchoue = new PlanificationEntretienEchoué(entretien);
        _messageBus.Send(plannificationEchoue);
        return plannificationEchoue;
    }
}