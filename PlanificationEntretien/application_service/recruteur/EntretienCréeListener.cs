using Planification_Entretien.domain;
using PlanificationEntretien.domain.entretien;
using PlanificationEntretien.domain.recruteur;

namespace PlanificationEntretien.application_service.recruteur;

public class EntretienCréeListener : Listener
{
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly MessageBus _messageBus;

    public EntretienCréeListener(IRecruteurRepository recruteurRepository, MessageBus messageBus) {
        _recruteurRepository = recruteurRepository;
        _messageBus = messageBus;
        _messageBus.Subscribe(this);
    }

    public void OnMessage(Event entretienCréé)
    {
        var recruteur = _recruteurRepository.FindById((entretienCréé as EntretienCréé)!.RecruteurId);
        if (recruteur != null) 
        {
            recruteur.RendreIndisponible();
            _recruteurRepository.Save(recruteur);
        }
    }
}