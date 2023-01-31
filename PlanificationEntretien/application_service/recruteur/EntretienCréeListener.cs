using Planification_Entretien.domain;
using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.application_service.recruteur;

public class EntretienCréeListener : Listener
{
    private readonly RendreRecruteurIndisponible _rendreRendreRecruteurIndisponible;
    private readonly MessageBus _messageBus;

    public EntretienCréeListener(RendreRecruteurIndisponible rendreRecruteurIndisponible, MessageBus messageBus) {
        _rendreRendreRecruteurIndisponible = rendreRecruteurIndisponible;
        _messageBus = messageBus;
        _messageBus.Subscribe(this);
    }

    public void OnMessage(Event entretienCréé)
    {
        _rendreRendreRecruteurIndisponible.Execute((entretienCréé as EntretienCréé)!.RecruteurId);
    }
}