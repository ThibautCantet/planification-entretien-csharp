using PlanificationEntretien.Common.ApplicationService;
using PlanificationEntretien.Common.Domain;
using PlanificationEntretien.Entretien.ApplicationService;
using PlanificationEntretien.Entretien.Domain;

namespace PlanificationEntretien.Recruteur.ApplicationService;

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