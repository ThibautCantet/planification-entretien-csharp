namespace PlanificationEntretien.Common.Cqrs.Middleware.Query;

public class QueryBusFactory
{

    public IQueryBus Build()
    {
        return new QueryBusDispatcher();
    }
}
