using PlanificationEntretien.common.cqrs.middleware.query;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class QueryBusFactory
{
    private readonly IEntretienDao _entretienDao;

    public QueryBusFactory(IEntretienDao entretienDao)
    {
        _entretienDao = entretienDao;
    }

    public IQueryBus Build()
    {
        return new QueryBusDispatcher(_entretienDao);
    }
}
