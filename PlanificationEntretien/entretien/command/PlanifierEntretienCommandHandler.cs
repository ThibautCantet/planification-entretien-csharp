using System;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.application_service;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.entretien.application_service;

public class PlanifierEntretienCommandHandler : ICommandHandler<PlanifierEntretienCommand, CommandResponse>
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

    public CommandResponse Handle(PlanifierEntretienCommand command)
    {
        var entretien = new Entretien(command.Candidat, command.Recruteur);
        var resultat = entretien.Planifier(command.DisponibiliteDuCandidat, command.DisponibiliteDuRecruteur);
        var entretienCréé = resultat as EntretienCréé;
        if (entretienCréé != null)
        {
            var entretienId = _entretienRepository.Save(entretien);
            _emailService.EnvoyerUnEmailDeConfirmationAuCandidat(command.Candidat.Email, command.DisponibiliteDuRecruteur);
            _emailService.EnvoyerUnEmailDeConfirmationAuRecruteur(command.Recruteur.Email, command.DisponibiliteDuRecruteur);
            _messageBus.Send(new EntretienCréé(entretienId, command.Recruteur.Id));
            resultat = entretienCréé.UpdateId(entretienId);
        }

        return new CommandResponse(resultat);
    }

    public Type ListenTo()
    {
        return typeof(PlanifierEntretienCommand);
    }
}