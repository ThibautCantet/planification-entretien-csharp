using com.soat.planification_entretien.common.cqrs.query;
using PlanificationEntretien.common.cqrs.query;

namespace PlanificationEntretien.Common.Cqrs.Middleware.Query;

public class QueryBusDispatcher : IQueryBus
{

    public IQueryResponse<T> Dispatch<T>(IQuery query)
    {
        throw new UnmatchedQueryHandlerException(query);
    }
}
