using PlanificationEntretien.recruteur.application_service.application;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public class EventBusFactory
{
    private readonly IRecruteurDao _recruteurDao;

    public EventBusFactory(IRecruteurDao recruteurDao)
    {
        _recruteurDao = recruteurDao;
    }

    public IEventBus Build()
    {
        EventBusDispatcher eventBusDispatcher = new EventBusDispatcher(_recruteurDao);

        return new EventBusLogger(eventBusDispatcher);
    }
}
