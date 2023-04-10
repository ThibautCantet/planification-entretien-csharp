using System.Collections.Generic;
using com.soat.planification_entretien.common.cqrs.query;
using PlanificationEntretien.common.cqrs.middleware.query;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class QueryBusFactory
{
    protected List<IQueryHandler> GetQueryHandlers()
    {
        return new List<IQueryHandler>();
    }

    public IQueryBus Build()
    {
        List<IQueryHandler> queryHandlers = GetQueryHandlers();
        QueryBusDispatcher queryBusDispatcher = new QueryBusDispatcher(queryHandlers);

        return new QueryBusLogger(queryBusDispatcher);
    }
}
