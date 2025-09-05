using PlanificationEntretien.application_service;
using PlanificationEntretien.domain.entretien;
using Shared;

namespace PlanificationEntretien.use_case;

public class PlanificationReussiListener : Listener
{
    private readonly MessageBus _bus;
    private readonly RendreRecruteurIndisponible _rendreRecruteurIndisponible;

    public PlanificationReussiListener(MessageBus bus, RendreRecruteurIndisponible rendreRecruteurIndisponible)
    {
        _bus = bus;
        _rendreRecruteurIndisponible = rendreRecruteurIndisponible;
        _bus.Subscribe(this);
    }

    public void OnMessage(Event msg)
    {
        if (msg is EntretienPlanifie entretien)
        {
            _rendreRecruteurIndisponible.Execute(entretien.RecruteurEmail);
        }
    }
}