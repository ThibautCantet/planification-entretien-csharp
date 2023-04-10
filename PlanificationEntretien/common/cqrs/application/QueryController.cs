using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.Common.Cqrs.Middleware.Command;
using PlanificationEntretien.Common.Cqrs.Middleware.Query;

namespace PlanificationEntretien.Common.Cqrs.Application;

public abstract class QueryController : ControllerBase
{
    private IQueryBus _queryBus;
    protected readonly QueryBusFactory _queryBusFactory;

    public QueryController(QueryBusFactory queryBusFactory)
    {
        this._queryBusFactory = queryBusFactory;
    }

    protected IQueryBus GetQueryBus()
    {
        if (_queryBus == null)
        {
            this._queryBus = _queryBusFactory.Build();
        }
        return _queryBus;
    }
}