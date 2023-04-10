using PlanificationEntretien.common.cqrs.middleware.query;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class QueryBusFactory
{

    public IQueryBus Build()
    {
        return new QueryBusDispatcher();
    }
}
