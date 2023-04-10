using System;
using com.soat.planification_entretien.common.cqrs.query;
using PlanificationEntretien.common.cqrs.middleware.command;
using PlanificationEntretien.common.cqrs.query;

namespace PlanificationEntretien.common.cqrs.middleware.query;

public class QueryBusLogger : IQueryBus
{
    private readonly IQueryBus _queryBus;

    public QueryBusLogger(IQueryBus queryBus)
    {
        this._queryBus = queryBus;
    }

    public QueryResponse Dispatch(IQuery query)
    {
        var queryResponse = this._queryBus.Dispatch(query);
        Console.WriteLine(query.ToString());
        return queryResponse;
    }
}
