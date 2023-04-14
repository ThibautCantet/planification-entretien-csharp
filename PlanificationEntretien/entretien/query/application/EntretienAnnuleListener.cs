using System;
using PlanificationEntretien.common.cqrs.middleware.evt;
using PlanificationEntretien.entretien.command.domain;
using PlanificationEntretien.entretien.query.application;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.recruteur.application_service.application;

public class EntretienAnnuleListener : EventHandlerVoid<EntretienAnnulé>
{
    private readonly IEntretienProjectionDao _entretienProjectionDao;

    public EntretienAnnuleListener(IEntretienProjectionDao entretienProjectionDao) {
        this._entretienProjectionDao = entretienProjectionDao;
    }

    public override void Handle(EntretienAnnulé entretienAnnulé)
    {
        _entretienProjectionDao.IncrementEntretienAnnule();
    }

    public override Type ListenTo()
    {
        return typeof(EntretienAnnulé);
    }
}
