using PlanificationEntretien.recruteur.application_service.application;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public class EventBusFactory
{
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly IRecruteurDao _recruteurDao;

    public EventBusFactory(IRecruteurDao recruteurDao, IRecruteurRepository recruteurRepository)
    {
        _recruteurDao = recruteurDao;
        _recruteurRepository = recruteurRepository;
    }

    public IEventBus Build()
    {
        EventBusDispatcher eventBusDispatcher = new EventBusDispatcher(_recruteurDao, _recruteurRepository);

        return new EventBusLogger(eventBusDispatcher);
    }
}
