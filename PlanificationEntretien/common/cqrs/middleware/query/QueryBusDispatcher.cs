using com.soat.planification_entretien.common.cqrs.query;
using PlanificationEntretien.common.cqrs.middleware.command;
using PlanificationEntretien.common.cqrs.query;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.query;

public class QueryBusDispatcher : IQueryBus
{
    private readonly IEntretienDao _entretienDao;

    public QueryBusDispatcher(IEntretienDao entretienDao)
    {
        _entretienDao = entretienDao;
    }

    public IQueryResponse<T> Dispatch<T>(IQuery query)
    {
        if (query.GetType() == typeof(ListerEntretienQuery))
        {
            return (IQueryResponse<T>)new ListerEntretienQueryHandler(_entretienDao).Handle(query as ListerEntretienQuery);
        }
        throw new UnmatchedQueryHandlerException(query);
    }
}
