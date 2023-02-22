using System;
using System.Collections.Generic;
using PlanificationEntretien.application_service;
using PlanificationEntretien.domain;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.entretien.application_service;

public class PlanifierEntretienCommandHandler
{
    private readonly IEntretienRepository _entretienRepository;
    private readonly IEmailService _emailService;
    private readonly MessageBus _messageBus;

    public PlanifierEntretienCommandHandler(IEntretienRepository entretienRepository, IEmailService emailService, MessageBus messageBus)
    {
        _entretienRepository = entretienRepository;
        _emailService = emailService;
        _messageBus = messageBus;
    }

    public IEnumerable<Event> Handle(Candidat candidat, DateTime disponibiliteDuCandidat,
        Recruteur recruteur, DateTime disponibiliteDuRecruteur)
    {
        var entretien = new Entretien(candidat, recruteur);
        var resultat = entretien.Planifier(disponibiliteDuCandidat, disponibiliteDuRecruteur);
        var entretienCréé = resultat as EntretienCréé;
        if (entretienCréé != null)
        {
            var entretienId = _entretienRepository.Save(entretien);
            _emailService.EnvoyerUnEmailDeConfirmationAuCandidat(candidat.Email, disponibiliteDuRecruteur);
            _emailService.EnvoyerUnEmailDeConfirmationAuRecruteur(recruteur.Email, disponibiliteDuRecruteur);
            _messageBus.Send(new EntretienCréé(entretienId, recruteur.Id));
            resultat = entretienCréé.UpdateId(entretienId);
        }

        return new List<Event> { resultat };
    }
}