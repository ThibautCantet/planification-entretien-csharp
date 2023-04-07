using PlanificationEntretien.application_service;
using PlanificationEntretien.domain;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.recruteur.application_service.application;

public class RecruteurCréeListener : Listener
{
    private readonly IRecruteurDao recruteurDao;
    private readonly MessageBus messageBus;

    public RecruteurCréeListener(IRecruteurDao recruteurDao, MessageBus messageBus) {
        this.recruteurDao = recruteurDao;
        this.messageBus = messageBus;
        this.messageBus.Subscribe(this);
    }

    public void OnMessage(Event e)
    {
        var recruteurCrée = (RecruteurCrée)e;
        if (recruteurCrée.ExperienceInYears >= 10) {
            recruteurDao.AddExperimente(new RecruteurDetail(recruteurCrée.Id,
                    recruteurCrée.Language,
                    recruteurCrée.ExperienceInYears,
                    recruteurCrée.Email));
        }
    }
}
