using System;
using PlanificationEntretien.common.cqrs.middleware.evt;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.recruteur.application_service.application;

public class RecruteurCréeListener : EventHandlerVoid<RecruteurCrée>
{
    private readonly IRecruteurDao _recruteurDao;

    public RecruteurCréeListener(IRecruteurDao recruteurDao) {
        this._recruteurDao = recruteurDao;
    }

    public override void Handle(RecruteurCrée recruteurCrée)
    { 
        if (recruteurCrée.ExperienceInYears >= 10) {
            _recruteurDao.AddExperimente(new RecruteurDetail(recruteurCrée.Id,
                recruteurCrée.Language,
                recruteurCrée.ExperienceInYears,
                recruteurCrée.Email));
        }
    }

    public override Type ListenTo()
    {
        return typeof(RecruteurCrée);
    }
}
