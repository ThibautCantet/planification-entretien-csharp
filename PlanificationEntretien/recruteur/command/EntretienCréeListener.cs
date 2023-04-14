using System;
using PlanificationEntretien.common.cqrs.middleware.evt;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.recruteur.application_service;

namespace PlanificationEntretien.entretien.application_service;

public class EntretienCréeListener : EventHandlerVoid<EntretienCréé>
{
    private readonly RendreRecruteurIndisponibleCommandHandler _rendreRendreRecruteurIndisponibleCommandHandler;

    public EntretienCréeListener(RendreRecruteurIndisponibleCommandHandler rendreRecruteurIndisponibleCommandHandler) {
        _rendreRendreRecruteurIndisponibleCommandHandler = rendreRecruteurIndisponibleCommandHandler;
    }

    public override void Handle(EntretienCréé entretienCréé)
    {
        var recruteurId = entretienCréé.RecruteurId;
        _rendreRendreRecruteurIndisponibleCommandHandler.Handle(new RendreRecruteurIndisponibleCommand(recruteurId));
    }

    public override Type ListenTo()
    {
        return typeof(EntretienCréé);
    }
}