using PlanificationEntretien.entretien.query.application;
using PlanificationEntretien.recruteur.application_service.application;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public class EventBusFactory
{
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly IRecruteurDao _recruteurDao;
    private readonly IEntretienProjectionDao _entretienProjectionDao;

    public EventBusFactory(IRecruteurDao recruteurDao, IRecruteurRepository recruteurRepository, IEntretienProjectionDao entretienProjectionDao)
    {
        _recruteurDao = recruteurDao;
        _recruteurRepository = recruteurRepository;
        _entretienProjectionDao = entretienProjectionDao;
    }

    public IEventBus Build()
    {
        EventBusDispatcher eventBusDispatcher = new EventBusDispatcher(_recruteurDao, _recruteurRepository, _entretienProjectionDao);

        return new EventBusLogger(eventBusDispatcher);
    }
}
