using PlanificationEntretien.domain;
using PlanificationEntretien.application_service;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.recruteur.application_service;

namespace PlanificationEntretien.entretien.application_service;

public class EntretienCréeListener : Listener
{
    private readonly RendreRecruteurIndisponibleCommandHandler _rendreRendreRecruteurIndisponibleCommandHandler;
    private readonly MessageBus _messageBus;

    public EntretienCréeListener(RendreRecruteurIndisponibleCommandHandler rendreRecruteurIndisponibleCommandHandler, MessageBus messageBus) {
        _rendreRendreRecruteurIndisponibleCommandHandler = rendreRecruteurIndisponibleCommandHandler;
        _messageBus = messageBus;
        _messageBus.Subscribe(this);
    }

    public void OnMessage(Event entretienCréé)
    {
        var recruteurId = (entretienCréé as EntretienCréé)!.RecruteurId;
        _rendreRendreRecruteurIndisponibleCommandHandler.Handle(new RendreRecruteurIndisponibleCommand(recruteurId));
    }
}