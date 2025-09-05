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
    private readonly MessageBus _messageBus;
    private readonly TrouverRecruteurDisponible _trouverRecruteurDisponible;

    public PlanifierEntretien(IEntretienRepository entretienRepository,
        ICandidatEvalueDAO candidatDao,
        IEmailService emailService,
        MessageBus messageBus, 
        TrouverRecruteurDisponible trouverRecruteurDisponible)
    {
        _entretienRepository = entretienRepository;
        _candidatDao = candidatDao;
        _emailService = emailService;
        _messageBus = messageBus;
        _trouverRecruteurDisponible = trouverRecruteurDisponible;
    }

    public Event Execute(int candidatEvaluéId, DateTime disponibiliteDuCandidat, DateTime disponibiliteDuRecruteur)
    {
        var candidatEvalué = _candidatDao.FindById(candidatEvaluéId);
        var recruteurAssigné = _trouverRecruteurDisponible.Execute(candidatEvalué);

        if (recruteurAssigné is not null) {
            //récuperer l'entretien initialisé sans recruteur ni date d'entretien
            var entretien = new Entretien(candidatEvalué);
            
            entretien.Planifier(disponibiliteDuCandidat, recruteurAssigné);
            
            var entretienId = _entretienRepository.Save(entretien);
            _emailService.EnvoyerUnEmailDeConfirmationAuCandidat(candidatEvalué.Email, disponibiliteDuRecruteur);
            _emailService.EnvoyerUnEmailDeConfirmationAuRecruteur(recruteurAssigné.Email, disponibiliteDuRecruteur);
            var plannificationReussi = new EntretienPlanifie(entretienId, candidatEvalué.Email, recruteurAssigné.Email);
            _messageBus.Send(plannificationReussi);
            return plannificationReussi;
        }

        var plannificationEchoue = new PlanificationEntretienEchoué();
        _messageBus.Send(plannificationEchoue);
        return plannificationEchoue;
    }
}