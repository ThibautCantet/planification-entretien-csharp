using System;
using System.Collections.Generic;
using PlanificationEntretien.Common.ApplicationService;
using PlanificationEntretien.Common.Domain;
using PlanificationEntretien.Entretien.Domain;

namespace PlanificationEntretien.Entretien.ApplicationService;

public class PlanifierEntretien
{
    private readonly IEntretienRepository _entretienRepository;
    private readonly IEmailService _emailService;
    private readonly MessageBus _messageBus;

    public PlanifierEntretien(IEntretienRepository entretienRepository, IEmailService emailService, MessageBus messageBus)
    {
        _entretienRepository = entretienRepository;
        _emailService = emailService;
        _messageBus = messageBus;
    }

    public IEnumerable<Event> Execute(Domain.Candidat candidat, DateTime disponibiliteDuCandidat,
        Domain.Recruteur recruteur, DateTime disponibiliteDuRecruteur)
    {
        var entretien = new Domain.Entretien(candidat, recruteur);
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