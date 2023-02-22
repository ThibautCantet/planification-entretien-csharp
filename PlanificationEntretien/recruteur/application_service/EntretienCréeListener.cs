using PlanificationEntretien.domain;
using PlanificationEntretien.application_service;
using PlanificationEntretien.entretien.domain;

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
        _rendreRendreRecruteurIndisponibleCommandHandler.Handle((entretienCréé as EntretienCréé)!.RecruteurId);
    }
}