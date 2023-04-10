

using PlanificationEntretien.common.cqrs.middleware.command;

namespace com.soat.planification_entretien.common.cqrs.application;

public abstract class QueryController
{
    private IQueryBus _commandBus;
    private readonly QueryBusFactory _commandBusFactory;

    public QueryController(QueryBusFactory commandBusFactory)
    {
        this._commandBusFactory = commandBusFactory;
    }

    protected IQueryBus GetQueryBus()
    {
        if (_commandBus == null)
        {
            this._commandBus = _commandBusFactory.Build();
        }
        return _commandBus;
    }
}