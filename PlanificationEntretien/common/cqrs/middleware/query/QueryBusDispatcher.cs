using com.soat.planification_entretien.common.cqrs.query;
using PlanificationEntretien.common.cqrs.middleware.command;
using PlanificationEntretien.common.cqrs.query;

namespace PlanificationEntretien.common.cqrs.middleware.query;

public class QueryBusDispatcher : IQueryBus
{

    public IQueryResponse<T> Dispatch<T>(IQuery query)
    {
        throw new UnmatchedQueryHandlerException(query);
    }
}
