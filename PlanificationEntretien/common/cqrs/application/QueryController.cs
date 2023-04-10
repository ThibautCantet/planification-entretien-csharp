

using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.common.cqrs.middleware.command;

namespace com.soat.planification_entretien.common.cqrs.application;

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