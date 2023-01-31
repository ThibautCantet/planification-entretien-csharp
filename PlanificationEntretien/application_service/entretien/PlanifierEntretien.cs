using System;
using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.application_service.entretien;

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

    public int Execute(Candidat candidat, DateTime disponibiliteDuCandidat,
        Recruteur recruteur, DateTime disponibiliteDuRecruteur)
    {
        var entretien = new Entretien(candidat, recruteur);
        if (entretien.Planifier(disponibiliteDuCandidat, disponibiliteDuRecruteur))
        {
            var entretienId = _entretienRepository.Save(entretien);
            _emailService.EnvoyerUnEmailDeConfirmationAuCandidat(candidat.Email, disponibiliteDuRecruteur);
            _emailService.EnvoyerUnEmailDeConfirmationAuRecruteur(recruteur.Email, disponibiliteDuRecruteur);
            _messageBus.Send(new EntretienCréé(entretien.Id, recruteur.Id));

            return entretienId;
        }

        return -1;
    }
}